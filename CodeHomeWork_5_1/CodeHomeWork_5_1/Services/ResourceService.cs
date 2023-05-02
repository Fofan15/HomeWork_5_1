using CodeHomeWork_5_1.Config;
using CodeHomeWork_5_1.Dtos.Responses;
using CodeHomeWork_5_1.Dtos;
using CodeHomeWork_5_1.Services.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net;

namespace CodeHomeWork_5_1.Services
{
    public class ResourceService : IResourceService
    {
        private readonly IInternalHttpClientService _httpClientService;
        private readonly ILogger<UserService> _logger;
        private readonly ApiOption _options;
        private readonly string _resourceApi = "api/unknown/";

        public ResourceService(
            IInternalHttpClientService httpClientService,
            IOptions<ApiOption> options,
            ILogger<UserService> logger)
        {
            _httpClientService = httpClientService;
            _logger = logger;
            _options = options.Value;
        }

        public async Task<ResourceListResponse> GetResourceList()
        {
            var services = new ServiceCollection();
            services.AddHttpClient();
            var serviceprovider = services.BuildServiceProvider();
            var httpClientFactoryr = serviceprovider.GetService<IHttpClientFactory>();
            var httpClient = httpClientFactoryr.CreateClient();

            var response = await httpClient.GetAsync("https://reqres.in/api/unknown");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ResourceListResponse>(content);
                return result;
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                _logger.LogError($"Error");
                return null;
            }
            else
            {
                throw new Exception($"Failed to get resource list: {response.StatusCode}");
            }
        }

        public async Task<ResourceDto> GetResourceById(int id)
        {
            var result = await _httpClientService.SendAsync<BaseResponse<ResourceDto>, object>($"{_options.Host}{_resourceApi}{id}", HttpMethod.Get);

            if (result?.Data != null)
            {
                _logger.LogInformation($"Resource with id = {result.Data.Id} was found");
            }

            return result?.Data;
        }
    }
}
