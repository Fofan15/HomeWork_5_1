using CodeHomeWork_5_1.Config;
using CodeHomeWork_5_1.Dtos.Requests;
using CodeHomeWork_5_1.Dtos.Responses;
using CodeHomeWork_5_1.Dtos;
using CodeHomeWork_5_1.Services.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Net;

namespace CodeHomeWork_5_1.Services
{
    public class UserService : IUserService
    {
        private readonly IInternalHttpClientService _httpClientService;
        private readonly ILogger<UserService> _logger;
        private readonly ApiOption _options;
        private readonly string _userApi = "api/users/";

        public UserService(
            IInternalHttpClientService httpClientService,
            IOptions<ApiOption> options,
            ILogger<UserService> logger)
        {
            _httpClientService = httpClientService;
            _logger = logger;
            _options = options.Value;
        }

        public  async Task<PageResponse<UserDto>> GetUsersList()
        {
            var result = await _httpClientService.SendAsync<PageResponse<UserDto>, object>($"{_options.Host}{_userApi}", HttpMethod.Get);

            if (result?.Data != null)
            {
                _logger.LogInformation($"Users list was found {result.Data}");
            }

            return result;
        }

        public async Task<UserDto> GetUserById(int id)
        {
            var result = await _httpClientService.SendAsync<BaseResponse<UserDto>, object>($"{_options.Host}{_userApi}{id}", HttpMethod.Get);

            if (result?.Data != null)
            {
                _logger.LogInformation($"User with id = {result.Data.Id} was found");
            }

            return result?.Data;
        }


        public async Task<UserResponseCreate> CreateUser(string name, string job)
        {
            var result = await _httpClientService.SendAsync<UserResponseCreate, UserRequest>(
                $"{_options.Host}{_userApi}",
                HttpMethod.Post,
                new UserRequest()
                {
                    Job = job,
                    Name = name
                });

            if (result != null)
            {
                _logger.LogInformation($"User with id = {result?.Id} was created");
            }

            return result;
        }

        public async Task<UserResponseUpdate> UpdateUser(int id, string name, string job)
        {
            var result = await _httpClientService.SendAsync<UserResponseUpdate, UserRequest>(
                $"{_options.Host}{_userApi}{id}",
                HttpMethod.Put,
                new UserRequest()
                {
                    Job = job,
                    Name = name
                });

            if (result != null)
            {
                _logger.LogInformation($"User with id = {id} was updated");
            }

            return result;
        }

        public async Task<UserResponseUpdate> UpdateUserPatch(int id, string name, string job)
        {
            var result = await _httpClientService.SendAsync<UserResponseUpdate, UserRequest>(
                $"{_options.Host}{_userApi}{id}",
                HttpMethod.Patch,
                new UserRequest()
                {
                    Job = job,
                    Name = name
                });

            if (result != null)
            {
                _logger.LogInformation($"User with id = {id} was updated");
            }

            return result;
        }

        public async Task<UserResponseCreate> DeleteUser(int id)
        {
            var result = await _httpClientService.SendAsync<UserResponseCreate, UserRequest>(
                $"{_options.Host}{_userApi}",
                HttpMethod.Delete);

            if (result != null)
            {
                _logger.LogInformation($"User with id = {id} was deleted");
            }

            return result;
        }
    }
}
