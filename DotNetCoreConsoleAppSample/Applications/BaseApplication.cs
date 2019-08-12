using Microsoft.Extensions.Logging;
using System;

namespace DotNetCoreConsoleAppSample.Applications
{
    public interface IApplication
    {
        void Run();
    }

    public abstract class BaseApplication : IApplication
    {
        protected readonly ILogger _logger;

        public BaseApplication(ILogger<Application> logger)
        {
            _logger = logger;
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
        protected virtual void Before()
        {
            _logger.LogInformation("処理開始");
        }

        /// <summary>
        /// メイン処理
        /// </summary>
        protected abstract void Main();

        /// <summary>
        /// 事後処理
        /// </summary>
        protected virtual void After()
        {
            _logger.LogInformation("処理完了");
        }
    }
}
