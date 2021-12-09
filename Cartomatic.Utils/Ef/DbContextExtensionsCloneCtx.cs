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
    /// <summary>
    /// DbContext extensions
    /// </summary>
    public static partial class DbContextExtensions
    {

        /// <summary>
        /// clones db context; requires the cloned ctx to implement a ctor that takes in 2 params (DbConnection conn and bool contextOwnsConnection);
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="contextOwnsConnection"></param>
        /// <returns></returns>
        public static DbContext Clone(this DbContext ctx, bool contextOwnsConnection = true)
        {
            //need to clone the connection too as have no idea how the connection has been provided to the ctx - directly, via conn str name as conn str, etc.
#if NETSTANDARD2_0 || NETCOREAPP3_1 || NET5_0_OR_GREATER || NET6_0_OR_GREATER
            var clonedConn = (DbConnection)Activator.CreateInstance(ctx.Database.GetDbConnection().GetType());
            clonedConn.ConnectionString = ctx.Database.GetDbConnection().ConnectionString;
#endif

#if NETFULL
            var clonedConn = (DbConnection)Activator.CreateInstance(ctx.Database.Connection.GetType());
            clonedConn.ConnectionString = ctx.Database.Connection.ConnectionString;
#endif

            //this is where the ctx type is expected to provide a constructor that takes in DbConnection conn and bool contextOwnsConnection
            DbContext clonedCtx = null;
            try
            {
                clonedCtx =
                    (DbContext)Activator.CreateInstance(ctx.GetType(), new object[] { clonedConn, contextOwnsConnection });
            }
            catch
            {
                //ignore
            }

            if (clonedCtx == null)
            {
                try
                {
                    //ok looks, like ctx does not implement a (DbConnection conn and bool contextOwnsConnection) ctor
                    //need to mess a bit more.
                    //this time need to dig a bit deeper and get a conn str name

                    //set a read only conn prop now...
                    //looks like the 
                    var internalCtx = ctx.GetType().GetProperty("InternalContext", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(ctx);
                    var connStrName = (string)internalCtx.GetType()
                        .GetProperty("ConnectionStringName", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).GetValue(internalCtx);

                    //try to create a ctx with a conn str name
                    clonedCtx = (DbContext)Activator.CreateInstance(ctx.GetType(), new object[] { connStrName });

                    //Note: should set the ctx owns connection, but totally not sure where to find it...
                }
                catch
                {
                    //ignore
                }
            }

            if (clonedCtx == null)
            {
                throw new InvalidOperationException("It was not possible to clone the db context.");
            }

            return clonedCtx;
        }

    }
}
