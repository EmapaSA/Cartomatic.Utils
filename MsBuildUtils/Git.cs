using System;
using System.Diagnostics;
using System.IO;

#if NETSTANDARD2_0 || NETCOREAPP3_0

namespace MSBuildTasks
{
    /// <summary>
    /// Git utils - Copy from cartomatic.Utils.Git, so can execute ms build tasks flexibly without unneeded dependencies
    /// </summary>
    public partial class Git
    {

        private const string DefaultFileName = "version";

        /// <summary>
        /// Gets a version of a git repository for a specified repo path
        /// </summary>
        /// <param name="gitPath"></param>
        /// <returns></returns>
        public static string GetRepoVersion(string gitPath = null)
        {
            string apiVersion = null;

            gitPath = (gitPath ?? string.Empty).SolvePath();
            if (Directory.Exists(gitPath))
            {
                try
                {
                    apiVersion = Git.GetGitBasedVersion(gitPath);
                }
                catch
                {
                    //looks like we're not 'gitting' here, try to read it off a file...
                    //if a file os present fcors...
                    apiVersion = Git.GetGitVersionFromFile(AppDomain.CurrentDomain.BaseDirectory);
                }
            }

            return string.IsNullOrWhiteSpace(apiVersion) ? "unknown-version" : apiVersion;
        }

        /// <summary>
        /// Dumps a git version to a specified file or directory. if a file name cannot be worked out, a default file name is used
        /// </summary>
        /// <param name="gitPath"></param>
        /// <param name="outPath"></param>
        public static void DumpGitVersionToFile(string gitPath = null, string outPath = null)
        {
            var gitVersion = GetRepoVersion(gitPath);
            var outDir = GetVersionFileIoDirName(outPath);
            var outFile = GetVersionFileIoFileName(outPath);

            try
            {
                if (!Directory.Exists(outDir))
                    Directory.CreateDirectory(outDir);

                File.WriteAllText(outFile, gitVersion);
            }
            catch
            {
                //ignore
            }
        }

        /// <summary>
        /// Gets VersionFile IO dir
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static string GetVersionFileIoDirName(string path)
        {
            //make the path absolute
            path = (path ?? "").SolvePath();

            return path.IsDirectory() ? path : System.IO.Path.GetDirectoryName(path);
        }

        /// <summary>
        /// Gets a full file path of VersionFile io
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static string GetVersionFileIoFileName(string path)
        {
            //make the path absolute
            path = (path ?? "").SolvePath();

            return path.IsDirectory() ? System.IO.Path.Combine(path, DefaultFileName) : path;
        }

        /// <summary>
        /// extracts version off a git repo
        /// </summary>
        /// <param name="gitPath"></param>
        /// <returns></returns>
        protected static string GetGitBasedVersion(string gitPath)
        {
            var step0 = ExecuteGitCommand(gitPath, "--version");
            if (string.IsNullOrWhiteSpace(TrimLineEnds(step0.output)))
                return "invalid-git-repo";

            //tag if any
            var step1 = ExecuteGitCommand(gitPath, "describe --tags --abbrev=0");
            var tag = TrimLineEnds(step1.output);
            if (string.IsNullOrEmpty(tag))
                tag = "not-tagged";

            //commit count since tag
            var commitCount = "rev-list --count HEAD";
            if (tag != "not-tagged")
                commitCount = $"rev-list --count {tag}^..HEAD";
            var step2 = ExecuteGitCommand(gitPath, commitCount);
            commitCount = TrimLineEnds(step2.output);

            //hash of last commit
            var step3 = ExecuteGitCommand(gitPath, "rev-parse --short HEAD");
            var hash = TrimLineEnds(step3.output);

            return $"{tag.Replace(".", "_")}-{commitCount}-{hash}";

        }


        /// <summary>
        /// reads a version sring from a file 
        /// </summary>
        /// <returns></returns>
        protected static string GetGitVersionFromFile(string filePath)
        {
            var file = GetVersionFileIoFileName(filePath);

            if (File.Exists(file))
                return File.ReadAllText(file);
            else
                return null;
        }

        /// <summary>
        /// Trims line ends
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string TrimLineEnds(string str)
        {
            return str.Replace("\n", "");
        }

        /// <summary>
        /// executes a git command
        /// </summary>
        /// <param name="path"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        private static (string output, string error, int? exitCode) ExecuteGitCommand(string path, string command)
        {
            var processInfo = new ProcessStartInfo("git.exe", command)
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                WorkingDirectory = path,
                RedirectStandardError = true,
                RedirectStandardOutput = true
            };

            var process = Process.Start(processInfo);
            process?.WaitForExit();

            var output = (output: process?.StandardOutput.ReadToEnd(), error: process?.StandardError.ReadToEnd(), exitCode: process?.ExitCode);

            process?.Close();

            return output;
        }
    }

}
#endif