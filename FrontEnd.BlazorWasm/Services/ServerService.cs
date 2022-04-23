using FrondEnd.Shared.DTOs;
using FrondEnd.Shared.Models;
using FrontEnd.BlazorWasm.Services.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace FrontEnd.BlazorWasm.Services
{
    public class ServerService : IServerService
    {

        private readonly HttpClient _httpClient;
        private readonly ILogger<ServerService> _logger;

        public ServerService(HttpClient httpClient, ILogger<ServerService> logger)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task<Response<string>> AuthenticateAsync(UserDTO user)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/Authentication/authenticate", user);
                if (response.IsSuccessStatusCode)
                {
                    var rawResponseData = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<Response<string>>(rawResponseData);
                    return data!;
                }

                _logger.LogWarning("The request returned with the status {0}", response.StatusCode);
                var rawError = await response.Content.ReadAsStringAsync();
                var error = JsonConvert.DeserializeObject<Response<string>>(rawError);
                return error!;
            }
            catch (Exception e)
            {
                _logger.LogError(new EventId(500, "internal error"), e, "An error occured when processing");
                return new Response<string>("ERROR", "Failed to process, something went wrong");
            }
        }
    }
}
