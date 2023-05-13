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
        private readonly IInternalHttpClientService _httpClientService;
        private readonly ILogger<RegisterService> _logger;
        private readonly ApiOption _options;
        private readonly string _registerApi = "api/register/";

        public RegisterService(
            IInternalHttpClientService httpClientService,
            IOptions<ApiOption> options,
            ILogger<RegisterService> logger)
        {
            _httpClientService = httpClientService;
            _logger = logger;
            _options = options.Value;
        }

        public async Task<RegisterResponse> RegisterUser(string email, string password)
        {
            var content = new FormUrlEncodedContent(new[]
                     {
                         new KeyValuePair<string, string>("email", email),
                         new KeyValuePair<string, string>("password", password)
                     });

            var result = await _httpClientService.SendAsync<RegisterResponse, RegisterRequest>(
                 $"{_options.Host}{_registerApi}",
                 HttpMethod.Post,
                 new RegisterRequest()
                 {
                     Email = email,
                     Password = password
                 });

            if (result?.Token != null)
            {
                _logger.LogInformation($"Registration successful with id: {result.Id} token: {result.Token}");
            }
            else
            {
                _logger.LogError($"Registration failed");
            }

            return result;
        }
    }
}
