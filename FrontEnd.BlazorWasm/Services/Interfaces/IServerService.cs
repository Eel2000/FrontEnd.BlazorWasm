using FrondEnd.Shared.DTOs;
using FrondEnd.Shared.Models;

namespace FrontEnd.BlazorWasm.Services.Interfaces
{
    public interface IServerService
    {
        /// <summary>
        /// Used to log into the app
        /// </summary>
        /// <param name="user"></param>
        /// <returns><see cref="Response{T}"/> The response obj</returns>
        Task<Response<string>> AuthenticateAsync(UserDTO user);
    }
}
