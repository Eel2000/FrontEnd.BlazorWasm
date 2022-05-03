﻿using FrondEnd.Shared.DTOs;
using FrondEnd.Shared.Models;
using FrontEnd.BlazorWasm.Services.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Json;
using FrondEnd.Shared.Utils;
using Newtonsoft.Json.Linq;
using System.Text;

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

        public async Task<OdataResponse> GetODataAsync(string token)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetFromJsonAsync<OdataResponse>("/getData?serviceName=Interventi");
                return response!;
            }
            catch (Exception e)
            {
                _logger.LogError(new EventId(500, "Internal error"), e, "Error while processing");
                return null!;
            }
        }

        public async Task<Response<IReadOnlyList<Products>>> GetProductsAsync(string token)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync("/api/Products/get-products");
                if (response.IsSuccessStatusCode)
                {
                    var rawData = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Response<IReadOnlyList<Products>>>(rawData)!;
                }
                var error = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Response<IReadOnlyList<Products>>>(error)!;
            }
            catch (Exception e)
            {
                _logger.LogError(new EventId(500, "Internal error"), e, "Error while processing");
                return new Response<IReadOnlyList<Products>>("ERROR", "Failed to get data. your session has experied.");
            }
        }

        public async Task<Response<DetailsDTO>> GetProductsDetailsAsync(string token, string id)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                //TODO: the de get_by_id api. to be implemented
                var response = await _httpClient.GetFromJsonAsync<DetailsDTO>($"/getData?serviceName=DettaglioIntervento&counter={id}");
                //return result
                return new Response<DetailsDTO>("SUCCESS", "details get");//this is jus a similation
            }
            catch (Exception e)
            {
                _logger.LogError(new EventId(500, "internal error"), e, "An error occured while processing");
                return new Response<DetailsDTO>("ERROR", "Failed to get result");
            }
        }

        public async Task<bool> DownloadProceess(string token, string id)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                //TODO: the de get_by_id api. to be implemented
                var response = await _httpClient.GetStreamAsync($"/getFile?id={id}");
                //return result
                return true;//this is jus a similation
            }
            catch (Exception e)
            {
                _logger.LogError(new EventId(500, "internal error"), e, "An error occured while processing");
                return false;
            }
        }

        public async Task<object> LoginAsync(UserDTO user)
        {
            try
            {
                var credentials = new Request
                {
                    username = user.Username,
                    password = user.Password
                };
                var body = new StringContent(JsonConvert.SerializeObject(credentials), Encoding.UTF8, "application/json");

                var resquest = await _httpClient.PostAsync("/getToken", body);
                if (resquest.IsSuccessStatusCode)
                {
                    var rawData = await resquest.Content.ReadAsStringAsync();
                    var data = JObject.Parse(rawData).GetValue("token");

                    return data!.ToString();
                }

                return null!;
            }
            catch (Exception e)
            {
                _logger.LogError(new EventId(500, "Api error"), e, "An error occured while processing");
                return e.Message;
            }
        }
    }
}
