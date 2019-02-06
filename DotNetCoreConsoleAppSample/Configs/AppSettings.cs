using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCoreConsoleAppSample.Configs
{
    public class AppSettings
    {
        public SampleSettings SampleSettings { get; set; }
    }

    public class SampleSettings
    {
        public string Key { get; set; }
    }
}
