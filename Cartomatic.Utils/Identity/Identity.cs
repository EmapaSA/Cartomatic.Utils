using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;

#if NETFULL
using System.Runtime.Remoting.Messaging;
using System.Web;
#endif

namespace Cartomatic.Utils
{
    /// <summary>
    /// Identity utils
    /// </summary>
    public static class Identity
    {
        /// <summary>
        /// subject of an identity claim
        /// </summary>
        public const string Subject = "sub";

        /// <summary>
        /// Gets a user uuid off the thread's CurrentPrincipal
        /// </summary>
        /// <returns></returns>
        public static Guid? GetUserGuid()
        {
            Guid? guid = null;

            try
            {
                var cp = ClaimsPrincipal.Current;
                //same as 
                //Thread.CurrentPrincipal as ClaimsPrincipal;
                //but will throw if the concrete IPrincipal is not ClaimsPrincipal

                //Mote:
                //in aspnet core ClaimsPrincipal.Current & are never set
                //https://docs.microsoft.com/en-us/aspnet/core/migration/claimsprincipal-current?view=aspnetcore-2.1
                //therefore for the sake of convenience of keeping on using this utility an attibute that explicitly impoersonates a user 'the old way' needs to be provided in a consuming aspnet core api

                //grab the sub claim
                var subjectClaim = cp?.FindFirst(Subject);

                

                //if no subject claim is present try to obtain it off the logical ctx
                //see the comments in ImpersonateGhostUser
                if (subjectClaim == null)
                {
#if NETFULL
                    cp = (ClaimsPrincipal) CallContext.GetData("CurrentPrincipal");
                    subjectClaim = cp.FindFirst(Subject);
#endif

#if NETSTANDARD2_0 || NETCOREAPP3_1
                    cp = (ClaimsPrincipal)CallContext.GetData("CurrentPrincipal");
                    subjectClaim = cp?.FindFirst(Subject);
#endif

                }

                if (subjectClaim != null)
                {
                    guid = Guid.Parse(subjectClaim.Value);
                }
            }
            catch
            {
                //ignore
                //it is the cast that can potentially fail
            }


            return guid;
        }

        /// <summary>
        /// Sets a new Claims Principal on the thread's CurrentPrincipal with an IdentityServer sub claim containing a default guid value - aka 'ghost' uuid
        /// </summary>
        public static void ImpersonateGhostUser()
        {
            ImpersonateUser();
        }

        /// <summary>
        /// Impersonates user with a specified guid
        /// </summary>
        public static void ImpersonateUser(Guid? guid = null)
        {
            var cp = GetBasicClaimsPrincipal(guid ?? default(Guid));
            ImpersonateUser(cp);
        }

        /// <summary>
        /// Impersonates a user using supplied id
        /// </summary>
        /// <param name="id"></param>
        public static void ImpersonateUser(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return;

            var cp = GetBasicClaimsPrincipal(id);
            ImpersonateUser(cp);
        }

        /// <summary>
        /// Impersonates user with a specified claims principal
        /// </summary>
        /// <param name="cp"></param>
        private static void ImpersonateUser(ClaimsPrincipal cp)
        {
            //and in the call ctx

            //Note:
            //basically starting with .NET 4.5 any changes done to the logical call context within an async method are discarded
            //as soon as the method completes.

            //"Stephen Cleary:
            //In.NET 4.5., async methods interact with the logical call context so that it will more properly flow with async methods.
            //...
            //In.NET 4.5, at the beginning of every async method, it activates a "copy-on-write" behavior for its logical call context.
            //When(if) the logical call context is modified, it will create a local copy of itself first.

            //Some more info:
            //http://stackoverflow.com/questions/16653308/why-is-an-await-task-yield-required-for-thread-currentprincipal-to-flow-corr/16654386#16654386
            //http://blog.stephencleary.com/2013/04/implicit-async-context-asynclocal.html
            //http://www.hanselman.com/blog/SystemThreadingThreadCurrentPrincipalVsSystemWebHttpContextCurrentUserOrWhyFormsAuthenticationCanBeSubtle.aspx

#if NETFULL
            //set cp on the thread
            Thread.CurrentPrincipal = cp;

            CallContext.LogicalSetData("CurrentPrincipal", cp);
#endif

#if NETSTANDARD2_0 || NETCOREAPP3_1
            CallContext.SetData("CurrentPrincipal", cp);
#endif
        }

        /// <summary>
        /// Gets a basic claims principal with one claim - sub
        /// </summary>
        /// <param name="id"></param>
        /// <param name="scheme"></param>
        /// <returns></returns>
        public static ClaimsPrincipal GetBasicClaimsPrincipal(Guid id, string scheme = null)
        {
            return GetBasicClaimsPrincipal(id.ToString(), scheme);
        }

        /// <summary>
        /// gets basic claims principal
        /// </summary>
        /// <param name="id"></param>
        /// <param name="scheme"></param>
        /// <returns></returns>
        public static ClaimsPrincipal GetBasicClaimsPrincipal(string id, string scheme = null)
        {
            var claims = new List<Claim>
            {
                new Claim(Subject, id)
            };
            return new ClaimsPrincipal(
                string.IsNullOrEmpty(scheme)
                    ? new ClaimsIdentity(claims)
                    : new ClaimsIdentity(claims, scheme)
            );
        }

        /// <summary>
        /// impersonates user via http context rather than call context
        /// </summary>
        public static void ImpersonateGhostUserViaHttpContext()
        {
            ImpersonateUserViaHttpContext();
        }

        /// <summary>
        /// Impersonates a user via HttpContext object. Useful when entering MapHive API via WebAPI
        /// </summary>
        public static void ImpersonateUserViaHttpContext(Guid? uuid = null)
        {
#if NETFULL
            HttpContext.Current.User = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new List<Claim>
                    {
                        new Claim(Subject, (uuid ?? default(Guid)).ToString())
                    }
                )
            );
#endif
        }
    }

}
