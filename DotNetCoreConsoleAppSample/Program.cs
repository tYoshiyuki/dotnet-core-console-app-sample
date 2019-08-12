using DotNetCoreConsoleAppSample.Applications;
using DotNetCoreConsoleAppSample.Configs;
using DotNetCoreConsoleAppSample.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.LayoutRenderers;
using System;
using System.IO;

namespace DotNetCoreConsoleAppSample
{
    class Program
    {
        private static IConfiguration Configuration { get; set; }

        static void Main(string[] args)
        {
            InitializeApplication().Run();
            NLog.LogManager.Shutdown();
        }

        private static IApplication InitializeApplication()
        {
            // 環境変数の読み込み
            string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (string.IsNullOrWhiteSpace(env))
            {
#if DEBUG
                env = "Development";
#else
                env = "Production"; 
#endif
            }

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            // ServiceCollectionの設定
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            return serviceCollection.BuildServiceProvider()
                .GetService<IApplication>();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // configファイルの読み込み
            services.Configure<AppSettings>(Configuration);
            services.AddSingleton(Configuration);

            // ログの設定       
            services.AddLogging(builder =>
            {
                builder.ClearProviders();
                builder.SetMinimumLevel(LogLevel.Trace);
                builder.AddNLog(Configuration);
            });
            LayoutRenderer.Register<BuildConfigLayoutRenderer>("buildConfiguration");

            // サービス設定
            services.AddTransient<IHelloService, HelloService>();

            // Applicationの設定
            services.AddTransient<IApplication, Application>();
        }
    }
}
