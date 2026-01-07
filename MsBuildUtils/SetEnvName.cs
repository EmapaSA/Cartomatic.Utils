using System.IO;

#if NETSTANDARD2_0 || NETCOREAPP3_0
using Microsoft.Build.Framework;

namespace MSBuildTasks
{

    public class SetEnvName : Microsoft.Build.Utilities.Task
    {
        private string DefaultEnvName { get; set; } = "Development";
        public string EnvName { get; set; }
        public string WebConfigDir { get; set; }

        public override bool Execute()
        {
            Log.LogMessage(MessageImportance.High, $"Setting EnvName to {EnvName} from {DefaultEnvName}...");

            var webCfg = Path.Combine(WebConfigDir, "web.config");

            if (!File.Exists(webCfg))
            {
                Log.LogMessage(MessageImportance.High, $"Could not find web.config at: {webCfg}");
                return true;
            }

            var fileTxt = File.ReadAllText(webCfg);

            fileTxt = fileTxt.Replace($"<environmentVariable name=\"ASPNETCORE_ENVIRONMENT\" value=\"{DefaultEnvName}\" />", $"<environmentVariable name=\"ASPNETCORE_ENVIRONMENT\" value=\"{EnvName}\" />");

            File.WriteAllText(webCfg, fileTxt);

            Log.LogMessage(MessageImportance.High, $"Env set!");

            return true;
        }
    }

}
#endif