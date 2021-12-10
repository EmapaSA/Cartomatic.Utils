using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cartomatic.Utils
{
    /// <summary>
    /// Path utils
    /// </summary>
    public static class PathExtensions
    {
        /// <summary>
        /// Check if a path is an absolute path; checks only local paths
        /// </summary>
        /// <param name="path"></param>
        public static bool IsAbsolute(this string path)
        {
            if (path.IndexOfAny(System.IO.Path.GetInvalidPathChars()) >= 0)
                throw new Exception("Invalid path chars");

            //Note:
            //System.IO.Path.GetFullPath should return exactly the same string if a path is absolute
            //Otherwise it will try to solve the paths differently:
            //* c:\test - is absolute and full path should be the same
            //* \test - is a rooted path (has a root -> \) and full path will be resolved to root drive of the executing assembly
            //* test\ - is a relative path and full path will be resolved starting at the root folder of the executing assembly
            return !string.IsNullOrEmpty(path) && string.Compare(path.Trim(), System.IO.Path.GetFullPath(path), StringComparison.OrdinalIgnoreCase) == 0;
        }

        /// <summary>
        /// Solves path - tries to make a path an absolute path; relative path are treated as relative to AppDomain.CurrentDomain.BaseDirectory
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string SolvePath(this string path)
        {
            string fixedPath = path;

            try
            {
                //if the path is not absolute (relative)
                if (!path.IsAbsolute()) //this will throw errors on an ivalid path
                {
                    var basePath = AppDomain.CurrentDomain.BaseDirectory;

#if NETCOREAPP || NETCOREAPP3_1 || NET5_0_OR_GREATER || NET6_0_OR_GREATER
                    basePath =
#if DEBUG
                        AppDomain.CurrentDomain.BaseDirectory;
#else
                        System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
#endif
#endif

                    //solve it as relative to the base of the app (make it absolute in fact)
                    fixedPath = System.IO.Path.Combine(basePath, path);

                }
            }
            catch
            {
                //path is invalid as threw errs on a call to is absolute -> getFullPath...
                fixedPath = null;
            }

            return fixedPath;
        }

        /// <summary>
        /// Checks whether a spe
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsDirectory(this string path)
        {
            //dr exists, so obviously a dir...
            if (System.IO.Directory.Exists(path))
                return true;

            //file, so not a dir...
            if (System.IO.File.Exists(path))
                return false;

            //neither an existing file or dir, so just check the extension an assume no extension means a dir
            return !System.IO.Path.HasExtension(path);
        }
    }
}
