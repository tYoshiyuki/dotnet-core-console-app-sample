using DotNetCoreConsoleAppSample.Configs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace DotNetCoreConsoleAppSample
{
    class Program
    {
        private static IConfiguration Configuration { get; set; }

        static void Main(string[] args)
        {
            // 環境変数の読み込み
            string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (string.IsNullOrWhiteSpace(env))
            {
                env = "Development";
            }

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env}.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            // ServiceCollectionの設定
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            serviceCollection.BuildServiceProvider()
                .GetService<Application>().Run();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // configファイルの読み込み
            services.Configure<AppSettings>(Configuration);
            services.AddSingleton(Configuration);

            // ログの設定       
            services.AddLogging(builder =>
            {
                builder.AddConfiguration(Configuration.GetSection("Logging"))
                    .AddConsole()
                    .AddDebug();
            });

            // Applicationの設定
            services.AddTransient<Application>();
        }
    }
}
