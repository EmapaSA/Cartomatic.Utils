#if NETSTANDARD2_0 || NETCOREAPP3_0
using Microsoft.Build.Framework;

namespace MSBuildTasks
{

    public class DumpGitVersion : Microsoft.Build.Utilities.Task
    {
        public string GitPath { get; set; }
        public string OutPath { get; set; }

        public override bool Execute()
        {
            Log.LogMessage(MessageImportance.High, $"Dumping GitVersion from {GitPath} to {OutPath}");
            Git.DumpGitVersionToFile(GitPath, OutPath);
            Log.LogMessage(MessageImportance.High, $"GitVersion dumped!");

            return true;
        }
    }
}
#endif