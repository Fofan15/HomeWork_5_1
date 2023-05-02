using CodeHomeWork_5_1.Dtos.Responses;
using CodeHomeWork_5_1.Services.Abstractions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeHomeWork_5_1.Services
{
    public class LogingService : ILogingService
    {
        private readonly ILogger<UserService> _logger;
        private readonly string _registerUrl = "https://reqres.in/api/login";
        private static readonly HttpClient _client = new HttpClient();

        public LogingService(ILogger<UserService> logger)
        {
            _logger = logger;
        }

        public async Task<LoginResponse> LoginUser(string email, string password)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("email", email),
                new KeyValuePair<string, string>("password", password)
            });

            var response = await _client.PostAsync(_registerUrl, content);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation($"Login successful");
            }
            else
            {
                _logger.LogError($"Login failed with status code {response.StatusCode}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var registerResponse = JsonConvert.DeserializeObject<LoginResponse>(responseContent);

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
