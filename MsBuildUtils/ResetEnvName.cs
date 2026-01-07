using System.IO;

#if NETSTANDARD2_0 || NETCOREAPP3_0
using Microsoft.Build.Framework;

namespace MSBuildTasks
{

    public class ResetEnvName : Microsoft.Build.Utilities.Task
    {
        private string DefaultEnvName { get; set; } = "Development";
        public string EnvName { get; set; }
        public string WebConfigDir { get; set; }

        public override bool Execute()
        {
            Log.LogMessage(MessageImportance.High, $"Resetting EnvName from {DefaultEnvName} to {EnvName}...");

            var webCfg = Path.Combine(WebConfigDir, "web.config");

            if (!File.Exists(webCfg))
            {
                Log.LogMessage(MessageImportance.High, $"Could not find web.config at: {webCfg}");
                return true;
            }

            var fileTxt = File.ReadAllText(webCfg);

            fileTxt = fileTxt.Replace($"<environmentVariable name=\"ASPNETCORE_ENVIRONMENT\" value=\"{EnvName}\" />", $"<environmentVariable name=\"ASPNETCORE_ENVIRONMENT\" value=\"{DefaultEnvName}\" />");

            File.WriteAllText(webCfg, fileTxt);

            Log.LogMessage(MessageImportance.High, $"Env reset!");

            return true;
        }
    }

}
#endif