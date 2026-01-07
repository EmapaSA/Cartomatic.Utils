using System;
using System.Collections.Generic;

namespace Cartomatic.Utils
{
    /// <summary>
    /// Logging utils
    /// </summary>
    public partial class Logging
    {

        /// <summary>
        /// Gets unified error info in a form of separate multiline string
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetUnifiedErrorInfoMultiLine(Exception e)
        {
            var errs = new List<string>
            {
                DateTime.Now.ToLongTimeString()
            };

            while (e != null)
            {
                errs.Add(e.Message);
                errs.Add(e.StackTrace);
                errs.Add(new string('-', 50));
                errs.Add(Environment.NewLine);

                e = e.InnerException;
            }

            errs.Add(new string('=', 150));
            errs.Add(Environment.NewLine);
            errs.Add(Environment.NewLine);

            return errs;
        }

        /// <summary>
        /// Gets a unified error msg
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string GetUnifiedErrorInfo(Exception e)
        {
            return string.Join(Environment.NewLine, GetUnifiedErrorInfoMultiLine(e));
        }
    }
}
