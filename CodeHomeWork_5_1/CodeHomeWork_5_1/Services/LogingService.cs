using CodeHomeWork_5_1.Config;
using CodeHomeWork_5_1.Dtos;
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
using System.Xml.Linq;

namespace CodeHomeWork_5_1.Services
{
    public class LogingService : ILogingService
    {
        private readonly IInternalHttpClientService _httpClientService;
        private readonly ILogger<LogingService> _logger;
        private readonly ApiOption _options;
        private readonly string _loginApi = "api/login/";

        public LogingService(
            IInternalHttpClientService httpClientService,
            IOptions<ApiOption> options,
            ILogger<LogingService> logger)
        {
            _httpClientService = httpClientService;
            _logger = logger;
            _options = options.Value;
        }

        public async Task<LoginResponse> LoginUser(string email, string password)
        {
            var result = await _httpClientService.SendAsync<LoginResponse, LoginRequest>(
                 $"{_options.Host}{_loginApi}",
                 HttpMethod.Post,
                 new LoginRequest()
                 {
                     Email = email,
                     Password = password
                 });

            if (result?.Token != null)
            {
                _logger.LogInformation($"Login was successful {result.Token}");
            }
            else 
            {
                _logger.LogError($"Error loging ");
            }

            return result;
        }
    }
}
