using System;
using System.Configuration;
using System.Linq;
using Newtonsoft.Json;
using Rollbar;

#if NETSTANDARD2_0 || NETCOREAPP3_1 || NET5_0_OR_GREATER || NET6_0_OR_GREATER
using Microsoft.Extensions.Configuration;
#endif

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
        public static void LogToRollbar(Exception e)
        {
            try
            {
                var logger = GetRollbarLogger();
                if (logger == null)
                    return;

                try
                {
                    logger.Error(e);
                }
                catch (Exception ex)
                {
                    LogExceptions(ex);
                }
            }
            catch
            {
                //ignore
            }
        }

        /// <summary>
        /// whether or not rollbar has been configured
        /// </summary>
        protected static bool RollbarConfigured = false;

        /// <summary>
        /// 
        /// </summary>
        protected static ILogger RollbarLogger { get; set; }

        /// <summary>
        /// configures an returns rollbar logger
        /// </summary>
        /// <returns></returns>
        protected static ILogger GetRollbarLogger()
        {
            if (RollbarConfigured)
                return RollbarLogger;

            RollbarConfiguration rollbarCfg = null;

#if NETSTANDARD2_0 || NETCOREAPP3_1
            rollbarCfg = Cartomatic.Utils.NetCoreConfig.GetNetCoreConfig()
                .GetSection(nameof(RollbarConfiguration))
                .Get<RollbarConfiguration>();
#endif

#if NETFULL
            try
            {
                rollbarCfg =
                    JsonConvert.DeserializeObject<RollbarConfiguration>(
                        ConfigurationManager.AppSettings[nameof(RollbarConfiguration)]);
            }
            catch
            {
                //ignore
            }
#endif


            if (rollbarCfg != null && !string.IsNullOrWhiteSpace(rollbarCfg.AccessToken))
            {
                try
                {
                    RollbarLocator.RollbarInstance.Configure(new RollbarLoggerConfig(
                        rollbarCfg.AccessToken,
                        Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
                    )
                    {
                        RollbarDeveloperOptions =
                        {
                         Enabled = (rollbarCfg.Environments?.Select(x => x.ToLower()) ?? new string[0]).Contains(
                             Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")?.ToLower())
                        }
                    });

                    RollbarLogger = RollbarLocator.RollbarInstance.Logger;
                }
                catch (Exception ex)
                {
                    LogExceptions(ex);
                }
            }
            else
            {
                LogExceptions(new Exception("Rollbar could not be configured."));
            }

            RollbarConfigured = true;

            return RollbarLogger;
        }

        /// <summary>
        /// rollbar config template
        /// </summary>
        public class RollbarConfiguration
        {
            /// <summary>
            /// rollbar access token for given app
            /// </summary>
            public string AccessToken { get; set; }

            /// <summary>
            /// environments to log
            /// </summary>
            public string[] Environments { get; set; }
        }
    }
}
