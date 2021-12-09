using System;
using System.Collections.Generic;
using System.Text;
using Cartomatic.Utils.Data;

#if NETFULL
using System.Data.Entity;
#endif

#if NETSTANDARD2_0 || NETCOREAPP3_1 || NET5_0_OR_GREATER || NET6_0_OR_GREATER
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
#endif

namespace Cartomatic.Utils.Ef
{
    /// <summary>
    /// Used to ensure hooks for obtaining a valid instance in scenarios, where one cannot be sure of a presence of specific constructors.
    /// This way a valid, properly configured context can be obtained
    /// </summary>
    public interface IProvideDbContextFactory
    {
        /// <summary>
        /// produces a db ctx with given cfg
        /// </summary>
        /// <param name="connStrName"></param>
        /// <param name="isConnStr"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        DbContext ProduceDbContextInstance(string connStrName = null, bool isConnStr = false, DataSourceProvider provider = DataSourceProvider.EfInMemory);

        /// <summary>
        /// produces a default db ctx
        /// </summary>
        /// <returns></returns>
        DbContext ProduceDefaultDbContextInstance();
    }
}
