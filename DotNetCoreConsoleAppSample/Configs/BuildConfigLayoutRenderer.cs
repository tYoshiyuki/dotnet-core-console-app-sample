using NLog;
using NLog.Config;
using NLog.LayoutRenderers;
using System.Text;

namespace DotNetCoreConsoleAppSample.Configs
{
    [LayoutRenderer("buildConfiguration")]
    [ThreadAgnostic]
    public class BuildConfigLayoutRenderer : LayoutRenderer
    {
        private string _buildconfig;
        private string GetBuildConfig()
        {
            if (_buildconfig != null)
            {
                return _buildconfig;
            }

#if DEBUG
            _buildconfig = "Debug";
#else
            buildconfig = "Release";
#endif
            return _buildconfig;
        }

        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            builder.Append(GetBuildConfig());
        }
    }
}
