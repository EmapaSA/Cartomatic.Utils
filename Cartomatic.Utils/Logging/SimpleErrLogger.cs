using System;
using System.IO;

namespace Cartomatic.Utils
{
    /// <summary>
    /// Logging utils
    /// </summary>
    public partial class Logging
    {
        /// <summary>
        /// Simplistic exception logger that also dumps all the inner exceptions along with their stack trace
        /// </summary>
        /// <param name="e"></param>
        public static void LogExceptions(Exception e)
        {
            try
            {
                var dir = "_err_log".SolvePath();
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                var errs = GetUnifiedErrorInfoMultiLine(e);
                
                System.IO.File.AppendAllLines(Path.Combine(dir, $"{DateTime.Now:yyyyMMdd}.custom.log"), errs);
            }
            catch
            {
                //ignore
            }
        }
    }
}
