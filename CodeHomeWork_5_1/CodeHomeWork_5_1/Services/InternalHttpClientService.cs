using CodeHomeWork_5_1.Services.Abstractions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CodeHomeWork_5_1.Services
{
    public class InternalHttpClientService : IInternalHttpClientService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<InternalHttpClientService> _logger;

        public InternalHttpClientService(IHttpClientFactory clientFactory, ILogger<InternalHttpClientService> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }

        public async Task<TResponse> SendAsync<TResponse, TRequest>(string url, HttpMethod method, TRequest content = null)
            where TRequest : class
        {
            var client = _clientFactory.CreateClient();

            var httpMessage = new HttpRequestMessage();
            httpMessage.RequestUri = new Uri(url);
            httpMessage.Method = method;

            if (content != null)
            {
                httpMessage.Content =
                    new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
            }

            var result = await client.SendAsync(httpMessage);

            if (result.IsSuccessStatusCode && result.StatusCode != HttpStatusCode.NoContent)
            {
                var resultContent = await result.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<TResponse>(resultContent);

                _logger.LogInformation($"Status {result.StatusCode}");

                return response!;
            }

            _logger.LogError($"Status {result.StatusCode}");

            return default(TResponse)!;
        }
    }
}
