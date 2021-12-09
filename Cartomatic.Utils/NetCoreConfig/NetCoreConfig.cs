using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if NETSTANDARD2_0 || NETCOREAPP3_1 || NET5_0_OR_GREATER || NET6_0_OR_GREATER
using Microsoft.Extensions.Configuration;
#endif

namespace Cartomatic.Utils
{
    /// <summary>
    /// Net core cfg reader utils
    /// </summary>
    public class NetCoreConfig
    {

#if NETSTANDARD2_0 || NETCOREAPP3_1 || NET5_0_OR_GREATER || NET6_0_OR_GREATER
        /// <summary>
        /// Gets a net core cfg supplied via json jsonCfgFileNames
        /// </summary>
        /// <returns></returns>
        public static IConfiguration GetNetCoreConfig(params string [] jsonCfgFileNames)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(System.AppContext.BaseDirectory)

                //TODO - should really start using customised cfg providers at some point instead of IHostingEnv

                //cfg can be provided via appsettings
                //this is a default behavior, so always add id
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true);

            foreach (var fNames in jsonCfgFileNames)
            {
                builder
                    .AddJsonFile($"{fNames}.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"{fNames}.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true);
            }
            
            return builder.Build();
        }
#endif
    }
}
