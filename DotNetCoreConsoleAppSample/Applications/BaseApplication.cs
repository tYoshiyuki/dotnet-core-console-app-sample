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
        protected readonly ILogger Logger;

        protected BaseApplication(ILogger<Application> logger)
        {
            Logger = logger;
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
                Logger.LogError(ex, "処理失敗");
                Logger.LogError(ex.Message);
                Logger.LogError(ex.StackTrace);
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
            Logger.LogInformation("処理開始");
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
            Logger.LogInformation("処理完了");
        }
    }
}
