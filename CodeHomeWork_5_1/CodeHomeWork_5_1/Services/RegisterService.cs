using CodeHomeWork_5_1.Config;
using CodeHomeWork_5_1.Dtos.Requests;
using CodeHomeWork_5_1.Dtos.Responses;
using CodeHomeWork_5_1.Services.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeHomeWork_5_1.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly ILogger<UserService> _logger;
        private readonly string _registerUrl = "https://reqres.in/api/register";
        private static readonly HttpClient _client = new HttpClient();

        public RegisterService( ILogger<UserService> logger)
        {
            _logger = logger;
        }

        public async Task<RegisterResponse> RegisterUser(string email, string password)
        {
            var content = new FormUrlEncodedContent(new[] 
            {
                new KeyValuePair<string, string>("email", email),
                new KeyValuePair<string, string>("password", password)
            });

            var response = await _client.PostAsync(_registerUrl, content);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation($"Registration successful"); 
            }
            else
            {
                _logger.LogError($"Registration failed with status code {response.StatusCode}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var registerResponse = JsonConvert.DeserializeObject<RegisterResponse>(responseContent);

            return registerResponse;
        }

        public async Task<string> RegisterUserMissPassword(string email)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("email", email),   
            });

            var response = await _client.PostAsync(_registerUrl, content);

            if (!response.IsSuccessStatusCode) 
            {
                var error = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Error {response.StatusCode}");
                return error;
            }

            var responseContent = await response.Content.ReadAsStringAsync();

            return responseContent;
        }
    }
}
