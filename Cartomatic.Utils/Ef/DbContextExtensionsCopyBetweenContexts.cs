using System;
using System.Collections.Generic;
using System.Data.Common;

using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

#if NETFULL
using System.Data.Entity;
#endif

#if NETSTANDARD2_0 || NETCOREAPP3_1 || NET5_0_OR_GREATER || NET6_0_OR_GREATER
using Microsoft.EntityFrameworkCore;
#endif

namespace Cartomatic.Utils.Ef
{
    public static partial class DbContextExtensions
    {
        /// <summary>
        /// Copies a specified type from one db ctx to another
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbCtxFrom"></param>
        /// <param name="dbCtxTo"></param>
        /// <returns></returns>
        public static async Task CopyToAsync<T>(this DbContext dbCtxFrom, DbContext dbCtxTo)
            where T : class
        {
            var dbSetFrom = dbCtxFrom.Set<T>();
            var dbSetTo = dbCtxTo.Set<T>();

            var objectsToCopy = await dbSetFrom.ToListAsync();

            dbSetTo.RemoveRange(dbSetTo);

            foreach (var entity in objectsToCopy)
            {
                dbCtxFrom.Entry(entity).State = EntityState.Detached;
                dbSetTo.Add(entity);
            }

            await dbCtxTo.SaveChangesAsync();
        }
    }
}
