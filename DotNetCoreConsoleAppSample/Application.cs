using DotNetCoreConsoleAppSample.Applications;
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
    public class Application : BaseApplication
    {
        private readonly AppSettings _appSettings;
        private readonly IHelloService _service;

        public Application (ILogger<Application> logger, IOptions<AppSettings> optionsAccessor, IHelloService service) : base(logger)
        {
            _appSettings = optionsAccessor.Value;
            _service = service;
        }

        /// <summary>
        /// メイン処理
        /// </summary>
        protected override void Main()
        {
            Console.WriteLine(_service.Greeting());
            Console.WriteLine(_appSettings.SampleSettings.Key); // Sample Code
        }
    }
}
