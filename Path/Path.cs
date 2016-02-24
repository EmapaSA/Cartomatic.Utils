﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cartomatic.Utils.Path
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
                    //solve it as relative to the base of the app (make it absolute in fact)
                    fixedPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
                }
            }
            catch
            {
                //path is invalid as threw errs on a call to is absolute -> getFullPath...
                fixedPath = null;
            }

            return fixedPath;
        }
    }
}
