
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.Serialization;
using Cartomatic.Utils.Data;
#if NETSTANDARD2_0 || NETCOREAPP3_1
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
#endif

namespace Cartomatic.Utils.Ef
{
#if NETSTANDARD2_0 || NETCOREAPP3_1
#pragma warning disable 1591
    public static class DbContextFactory
    {
#pragma warning restore 1591
        private static IConfiguration Configuration { get; set; }

        static DbContextFactory()
        {
            Configuration = Cartomatic.Utils.NetCoreConfig.GetNetCoreConfig();
        }

        private static readonly Dictionary<DataSourceProvider, Action<DbContextOptionsBuilder, string>> ProvidersConfiguration =
            new Dictionary<DataSourceProvider, Action<DbContextOptionsBuilder, string>>
            {
                {
                    DataSourceProvider.EfInMemory,
                    (builder, connStr) => { builder.UseInMemoryDatabase(connStr); }
                },
                {
                    DataSourceProvider.Npgsql,
                    (builder, connStr) =>
                    {
                        builder.UseNpgsql(connStr);
                    }
                }
            };

        /// <summary>
        /// configures a data provider for given builder 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="provider"></param>
        /// <param name="connStr"></param>
        /// <returns></returns>
        public static DbContextOptionsBuilder ConfigureProvider(this DbContextOptionsBuilder builder,
            DataSourceProvider provider, string connStr)
        {
            if(!ProvidersConfiguration.ContainsKey(provider) || ProvidersConfiguration[provider] == null)
                throw new ArgumentException($"Db provider not configured: {provider}");

            ProvidersConfiguration[provider](builder, connStr);

            return builder;
        }

        /// <summary>
        /// Sets provider configuration
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="providerConfig"></param>
        public static void SetDataSourceProvider(DataSourceProvider provider, Action<DbContextOptionsBuilder, string> providerConfig)
        {
            ProvidersConfiguration[provider] = providerConfig;
        }

        /// <summary>
        /// Creates DbContext with the specified connection string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connStrName">name of conn string to retrieve from configuration</param>
        /// <param name="isConnStr">whether ot not it is the actual connection string supplied</param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static T CreateDbContext<T>(string connStrName = null, bool isConnStr = false, DataSourceProvider provider = DataSourceProvider.EfInMemory)
            where T: DbContext
        {
            var ctxType = typeof(T);

            var newCtx = default(T);

            //Special treatment for ctxs implementing IProvideDbContextFactory
            //this is for contexts that may not provide a default paramless or connstr, provider constructors
            if (typeof(IProvideDbContextFactory).GetTypeInfo().IsAssignableFrom(typeof(T).Ge‌​tTypeInfo()))
            {
                //does not call ctor
                var facade = (IProvideDbContextFactory)CreateDbContextFacade<T>();
                if (!string.IsNullOrWhiteSpace(connStrName))
                {
                    newCtx = (T)facade.ProduceDbContextInstance(connStrName, isConnStr, provider);
                }
                else
                {
                    newCtx = (T)facade.ProduceDefaultDbContextInstance();
                }
            }
            else
            {
                var opts = GetDbContextOptions<T>(connStrName, isConnStr);

                newCtx = (T)Activator.CreateInstance(ctxType, new object[] { opts });
            }

            return newCtx;
        }

        /// <summary>
        /// Creates an unitialized db context facade
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T CreateDbContextFacade<T>()
            where T : DbContext
        {
            return (T)CreateDbContextFacade(typeof(T));
        }

        /// <summary>
        /// Creates an unitialized db context facade
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static object CreateDbContextFacade(Type t)
        {
            if (!typeof(DbContext).GetTypeInfo().IsAssignableFrom(t.Ge‌​tTypeInfo()))
            {
                throw new InvalidEnumArgumentException($"{t.FullName} can is not assignable to {nameof(DbContext)}");
            }
            return FormatterServices.GetUninitializedObject(t);
        }

        /// <summary>
        /// Gets DbContextOptions for a specified db context
        /// </summary>
        /// <param name="connStrName"></param>
        /// <param name="isConnStr"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static DbContextOptions<T> GetDbContextOptions<T>(string connStrName, bool isConnStr = false, DataSourceProvider provider = DataSourceProvider.EfInMemory)
            where T: DbContext
        {
            var optionsBuilder = new DbContextOptionsBuilder<T>();

            var connStr = GetConnStr(connStrName, isConnStr);

            if (string.IsNullOrWhiteSpace(connStr))
            {
                throw new ArgumentException($"Could not work out a non-empty connection string - {nameof(connStrName)}: {connStr}, {nameof(isConnStr)}: {isConnStr}, {nameof(provider)}: {provider}.");
            }

            optionsBuilder.ConfigureProvider(provider, connStr);

            return optionsBuilder.Options;
        }

        /// <summary>
        /// Gets a conn str; obtains it from cfg if needed
        /// </summary>
        /// <param name="connStrName"></param>
        /// <param name="isConnStr"></param>
        /// <returns></returns>
        public static string GetConnStr(string connStrName, bool isConnStr = false)
        {
            //use a specified connection string or grab it from the cfgs
            if (isConnStr)
                return connStrName;

            var connStr = Configuration.GetConnectionString(connStrName);
            if (string.IsNullOrEmpty(connStr))
            {
                connStr = $"dummy-empty-conn-string :: could not obtain a non-empty connection string for a cfg key: {connStrName}";
            }

            return connStr;
        }
    }
#endif
}
