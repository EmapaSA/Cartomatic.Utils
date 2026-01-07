using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Cartomatic.Utils.Reflection
{
    /// <summary>
    /// Expression extensions
    /// </summary>
    public static class ExpressionExtensions
    {
        /// <summary>
        /// Returns property memebr info - extracts data off an expression that specifies a property
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static MemberInfo GetPropertyMemberInfoFromExpression<TObject, TProperty>(this Expression<Func<TObject, TProperty>> expression)
        {
            if (!(expression.Body is MemberExpression memberExp))
                throw new ArgumentException("Member does not exist.");

            var member = memberExp.Member;

            if (member.MemberType != MemberTypes.Property)
                throw new ArgumentException("Member is not a property.");

            return member;
        }
    }
}
