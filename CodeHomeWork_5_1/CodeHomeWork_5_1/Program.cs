using CodeHomeWork_5_1.Config;
using CodeHomeWork_5_1.Services.Abstractions;
using CodeHomeWork_5_1.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CodeHomeWork_5_1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            void ConfigureServices(ServiceCollection serviceCollection, IConfiguration configuration)
            {
                serviceCollection.AddOptions<ApiOption>().Bind(configuration.GetSection("Api"));
                serviceCollection
                    .AddLogging(configure => configure.AddConsole())
                    .AddHttpClient()
                    .AddTransient<IInternalHttpClientService, InternalHttpClientService>()
                    .AddTransient<IUserService, UserService>()
                    .AddTransient<IResourceService, ResourceService>()
                    .AddTransient<IRegisterService, RegisterService>()
                    .AddTransient<ILogingService, LogingService>()
                    .AddTransient<App>();
            }

            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("config.json")
                .Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection, configuration);
            var provider = serviceCollection.BuildServiceProvider();

            var app = provider.GetService<App>();
            await app!.Start();
        }
    }
}