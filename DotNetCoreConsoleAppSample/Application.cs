using DotNetCoreConsoleAppSample.Configs;
using DotNetCoreConsoleAppSample.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace DotNetCoreConsoleAppSample
{
    /// <summary>
    /// メイン処理クラス
    /// </summary>
    public class Application
    {
        private readonly ILogger _logger;
        private readonly AppSettings _appSettings;
        private readonly IHelloService _service;

        public Application (ILogger<Application> logger, IOptions<AppSettings> optionsAccessor, IHelloService service)
        {
            _logger = logger;
            _appSettings = optionsAccessor.Value;
            _service = service;
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
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
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
            Console.WriteLine(_service.Greeting());
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
