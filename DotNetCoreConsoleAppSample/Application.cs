using DotNetCoreConsoleAppSample.Configs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace DotNetCoreConsoleAppSample
{
    public class Application
    {
        private readonly ILogger _logger;
        private readonly AppSettings _appSettings;

        public Application (ILogger<Application> logger, IOptions<AppSettings> optionsAccessor)
        {
            _logger = logger;
            _appSettings = optionsAccessor.Value;
        }

        /// <summary>
        /// エントリポイント
        /// </summary>
        public void Run()
        {
            try
            {
                Before();

                Main();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "処理失敗");
            }
            finally
            {
                After();
            }
        }

        /// <summary>
        /// 事前処理
        /// </summary>
        private void Before()
        {
            _logger.LogInformation("処理開始");
        }

        /// <summary>
        /// メイン処理
        /// </summary>
        private void Main()
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine(_appSettings.SampleSettings.Key); // Sample Code
        }

        /// <summary>
        /// 事後処理
        /// </summary>
        private void After()
        {
            _logger.LogInformation("処理完了");
        }
    }
}
