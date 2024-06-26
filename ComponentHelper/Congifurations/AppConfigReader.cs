using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.ComponentHelper.Congifurations
{
    public class AppConfigReader
    {
        public AppConfigReader()
        {
            SetFrameworkSettings();
        }
        public static void SetFrameworkSettings()
        {
            var testEnv = Environment.GetEnvironmentVariable("QA");
            var b = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory());
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile($"appsettings.{testEnv}.json", optional: true)
                .Build();

            AppConfigSettings.Name = config.GetSection("AppConfigSettings").GetValue<string>("Name");
            AppConfigSettings.AppURL = config.GetSection("AppConfigSettings").GetValue<string>("AppURL");
            AppConfigSettings.BrowserName = config.GetSection("AppConfigSettings").GetValue<string>("BrowserName");
            AppConfigSettings.Username = config.GetSection("AppConfigSettings").GetValue<string>("Username");
            AppConfigSettings.Password = config.GetSection("AppConfigSettings").GetValue<string>("Password");
            AppConfigSettings.RunEnvironment = config.GetSection("AppConfigSettings").GetValue<string>("RunEnvironment");

        }
    }
}

