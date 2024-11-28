using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Cartomatic.Utils.Number;

#if NETFULL
using System.Data.Entity;
#endif

#if NETSTANDARD2_0 || NETCOREAPP3_1 || NET5_0_OR_GREATER || NET6_0_OR_GREATER
using Microsoft.EntityFrameworkCore;
#endif

namespace Cartomatic.Utils.Filtering
{
    public static partial class ReadFilterExtensions
    {
        /// <summary>
        /// Applies a read filter on an IQueryable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="filters"></param>
        /// <param name="greedy"></param>
        /// <returns></returns>
        public static IQueryable<T> ApplyReadFilters<T>(this IQueryable<T> query, IEnumerable<ReadFilter> filters,
            bool greedy = true)
        {
            var targetType = query.GetType().GetGenericArguments().FirstOrDefault();
            if (targetType == null || filters == null || !filters.Any())
                return query;


            //Main expression - encapsulates all the partial expressions joined by 'OR' (greedy) or 'AND' (!greedy / exact)
            Expression mainExpression = null;

            var paramExp = GetParameterExpression(targetType);

            //If a filter is marked as ExactMatch, then store it here and glue to the expression at the very end
            var exactMatch = new List<Expression>();


            //Create expression for each supplied filter
            foreach (var filter in filters)
            {
                var filterExpression = GetFilterExpression(filter, targetType, paramExp);
                

                //Note:
                //filter expression now has to be joined with previous filters BUT ONLY IF filter is not flagged with the ExactMatch. In such case this such filter is supposed
                //limit the resultset and be applied as AndAlso but at the very end of the Lambda Expression Tree, so it actually provides something like this:
                //(X AND / OR Y AND / OR Z) AND XX AND YY
                //http://stackoverflow.com/questions/6295926/how-build-lambda-expression-tree-with-multiple-conditions
                if (filter.ExactMatch)
                {
                    exactMatch.Add(filterExpression);
                }
                else
                {
                    mainExpression = mainExpression == null
                        ? filterExpression
                        : greedy
                            ? Expression.OrElse(mainExpression, filterExpression)
                            : Expression.AndAlso(mainExpression, filterExpression);
                }
            }

            //as all the filters have now been processed, add the exact match filters
            foreach (var filterExpression in exactMatch)
            {
                mainExpression = mainExpression == null
                    ? filterExpression
                    : Expression.AndAlso(mainExpression, filterExpression);
            }


            // If no expression generated return base query
            if (mainExpression == null)
                return query;


            //assemble a lambda that will be executed for the type in question
            var containsLambda = Expression.Lambda(mainExpression, paramExp);

            //make a param from the query / expression tree
            var targetAsConstant = Expression.Constant(query, query.GetType());

            //finally assemble the where body; this would translate to something like Where(p => Filtering expression for p value)
            var whereBody = Expression.Call(typeof(Queryable), "Where", new[] {targetType}, targetAsConstant,
                containsLambda);

            //debug
            //var test = whereBody.ToString();

            // Return query with filter expressions
            return query.Provider.CreateQuery<T>(whereBody);
        }

        /// <summary>
        /// Returns an expression representing a param to filter on; an equivalent of x => x. 
        /// Type expressess the typ of x
        /// </summary>
        /// <param name="targetType"></param>
        /// <returns></returns>
        private static ParameterExpression GetParameterExpression(Type targetType)
        {
            return Expression.Parameter(targetType, "p");
        }

        /// <summary>
        /// Gets a filter expression for a specified filter
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="targetType"></param>
        /// <param name="paramExp"></param>
        /// <returns></returns>
        private static Expression GetFilterExpression(ReadFilter filter, Type targetType, ParameterExpression paramExp)
        {
            Expression filterExpression = null;


            //adjust "guid" filter types to just "==", so they're filtered the same way
            if (filter.Operator == "guid" || filter.Operator == "!guid")
            {
                var nullableGuid = default(Guid?);
                if (!string.IsNullOrEmpty((string)filter.Value))
                {
                    if (!Guid.TryParse((string)filter.Value, out var guid))
                        throw new ArgumentException($"Filter on {filter.Property} with a value of {filter.Value} is specified as a guid filter; the value is nto parsable to guid though."); ;
                    nullableGuid = guid;
                }
    
                filter.Operator = filter.Operator == "guid" ? "==" : "!=";
                filter.Value = nullableGuid;
                filter.ExactMatch = true;
            }


            //if filter operator is not defined make it "==" for bools, "like" for strings and "eq" for numbers and dates
            //this is mainly because when filtering directly on store, operator is not sent and may be null
            if (string.IsNullOrEmpty(filter.Operator))
            {
                if (filter.Value is string)
                    filter.Operator = "like";

                else if (filter.Value is bool)
                    filter.Operator = "==";

                else if (filter.Value.IsNumeric() || filter.Value is DateTime)
                    filter.Operator = "eq";
            }

            //TODO - support some more filter operators such as =, <, <=, >, >=, notin, !=; this should be simply just a minor modification of the conditions below


            var propertyToFilterBy =
                targetType.GetProperties()
                    .FirstOrDefault(
                        p => string.Equals(p.Name, filter.Property, StringComparison.CurrentCultureIgnoreCase));

            if (filter.Operator != "nested")
            {
                //Check if model property exists; if not this is a bad, bad request...
                if (string.IsNullOrEmpty(filter.Property))
                    throw new ArgumentException(
                        $"The property to filter on has not been declared!");

                if (propertyToFilterBy == null)
                    throw new ArgumentException(
                        $"The property {targetType}.{filter.Property} is not defined for the model!");
            }


            //work out what sort of filtering should be applied for a property...

            //string filter
            if (filter.Operator == "like" && filter.Value is string)
            {
                //ExtJs sends string filters ike this:
                //{"operator":"like","value":"some value","property":"name"}

                //Note:
                //In some cases complex types are used to simplify interaction with them in an object oriented way (so through properties) and at the same time
                //such types are stored as a single json string entry in the database
                //in such case a type should implement IJsonSerializableType. I so it should be possible to filter such type as it was a string
                //(which it indeed is on the db side)
                if (propertyToFilterBy.PropertyType.GetInterfaces().Contains(typeof(Cartomatic.Utils.JsonSerializableObjects.IJsonSerializable)))
                {
                    //This will call to lower on the property that the filter applies to;
                    //pretty much means call ToLower on the property - something like p => p.ToLower()
                    var toLower = Expression.Call(
                        Expression.Property( //this specify the property to filter by on the param object specified earlier - so p => p.Property
                            Expression.Property(paramExp, filter.Property),
                            "serialized" //and we dig deeper here to reach p.Property.Serialised
                        ),
                        typeof(string).GetMethod("ToLower", Type.EmptyTypes)
                    //this is the method to be called on the property specified above
                    );

                    //finally need to assemble an equivaluent of p.Property.ToLower().Contains(filter.Value)
                    filterExpression = Expression.Call(toLower, "Contains", null,
                        Expression.Constant(((string)filter.Value.ToString()).ToLower(), typeof(string)));
                }
                else
                {
                    //this should be a string

                    //make sure the type of the property to filter by is ok
                    if (propertyToFilterBy.PropertyType != typeof(string))
                        throw new ArgumentException(
                            $"The property {targetType}.{propertyToFilterBy.Name} is not of 'string' type.");


                    //This will call to lower on the property that the filter applies to;
                    //pretty much means call ToLower on the property - something like p => p.ToLower()
                    var toLower = Expression.Call(
                        Expression.Property(paramExp, filter.Property),
                        //this specify the property to filter by on the param object specified earlier - so p => p.Property
                        typeof(string).GetMethod("ToLower", Type.EmptyTypes)
                    //this is the method to be called on the property specified above
                    );

                    //finally need to assemble an equivaluent of p.Property.ToLower().Contains(filter.Value)
                    filterExpression = Expression.Call(toLower, "Contains", null,
                        Expression.Constant(((string)filter.Value.ToString()).ToLower(), typeof(string)));
                }
            }

            //range filter
            else if (filter.Operator == "in" && filter.Value is JArray)
            {
                //{"operator":"in","value":[11,18],"property":"name"}

                try
                {
                    var json = ((string)filter.Value.ToString()).Trim().TrimStart('{').TrimEnd('}');
                    var list = JsonConvert.DeserializeObject<List<object>>(json);

                    foreach (var item in list)
                    {

                        Expression inExpression;

                        var underlyingType =
                            Nullable.GetUnderlyingType(Expression.Property(paramExp, filter.Property).Type);

                        if (
                            Expression.Property(paramExp, filter.Property).Type == typeof(Guid)
                            || underlyingType == typeof(Guid)
                            )
                        {
                            //property to string
                            //var toStr = Expression.Call(
                            //    GetFilteredProperty(filter, paramExp),
                            //    typeof(Guid).GetMethod("ToString", Type.EmptyTypes)
                            //);
                            ////and then lowercase
                            //var toLower = Expression.Call(typeof(string).GetMethod("ToString", Type.EmptyTypes));

                            //inExpression = Expression.Call(toLower,
                            //    typeof(string).GetMethod("Equals", new[] {typeof(string)}),
                            //    Expression.Constant(item.ToString().ToLower())
                            //);

                            //make sure can squize in nulls as filtered values
                            var nullableGuid = default(Guid?);
                            if (!string.IsNullOrEmpty((string) item))
                            {
                                if (!Guid.TryParse((string)item, out var guid))
                                    continue;
                                nullableGuid = guid;
                            }

                            inExpression = Expression.Equal(
                                GetFilteredProperty(filter, paramExp),
                                GetFilteredValue(filter, paramExp, nullableGuid, !nullableGuid.HasValue)
                            );
                        }
                        else
                        {
                            inExpression = Expression.Equal(
                                GetFilteredProperty(filter, paramExp),
                                GetFilteredValue(filter, paramExp, item)
                            );
                        }


                        //join the filters for each item in a list with OR
                        filterExpression = filterExpression == null
                            ? inExpression
                            : Expression.OrElse(filterExpression, inExpression);
                    }
                }
                catch (Exception ex)
                {
                    throw new ArgumentException("Property: " + propertyToFilterBy.Name + ", " + ex.Message,
                        ex.InnerException);
                }
            }

            //boolean filter
            //else if (filter.Operator == "==" && filter.Value is bool)
            else if (filter.Operator == "==")
            {
                //{"operator":"==","value":true,"property":"name"}

                //if (propertyToFilterBy.PropertyType != typeof(bool))
                //        throw new BadRequestException($"The property { targetType }.{ propertyToFilterBy.Name} is not of 'bool' type.");

                //filterExpression = Expression.Call(Expression.Property(expType, filter.Property), "Equals", null, Expression.Constant(filter.Value, typeof(bool)));

                filterExpression = Expression.Equal(
                    GetFilteredProperty(filter, paramExp),
                    GetFilteredValue(filter, paramExp)
                );
            }

            else if (filter.Operator == "!=")
            {
                //{"operator":"==","value":true,"property":"name"}

                //if (propertyToFilterBy.PropertyType != typeof(bool))
                //        throw new BadRequestException($"The property { targetType }.{ propertyToFilterBy.Name} is not of 'bool' type.");

                //filterExpression = Expression.Call(Expression.Property(expType, filter.Property), "Equals", null, Expression.Constant(filter.Value, typeof(bool)));

                filterExpression = Expression.NotEqual(
                    GetFilteredProperty(filter, paramExp),
                    GetFilteredValue(filter, paramExp)
                );
            }

            //Lower than / greater than / equals; applies to numbers and dates
            else if ((filter.Operator == "lt" || filter.Operator == "gt" || filter.Operator == "eq")
                     && (filter.Value is int || filter.Value is long || filter.Value is DateTime || filter.Value.IsNumeric()))
            {
                //{"operator":"lt","value":10,"property":"name"}
                //{"operator":"gt","value":10,"property":"name"}
                //{"operator":"eq","value":10,"property":"name"}
                //{"operator":"lt","value":"2016-05-08T22:00:00.000Z","property":"name"}
                //{"operator":"gt","value":"2016-05-08T22:00:00.000Z","property":"name"}
                //{"operator":"eq","value":"2016-05-08T22:00:00.000Z","property":"name"}

                try
                {
                    switch (filter.Operator)
                    {
                        case "eq":
                            filterExpression = Expression.Equal(
                                GetFilteredProperty(filter, paramExp),
                                GetFilteredValue(filter, paramExp)
                            );
                            break;
                        case "lt":
                            filterExpression = Expression.LessThan(
                                GetFilteredProperty(filter, paramExp),
                                GetFilteredValue(filter, paramExp)
                            );
                            break;
                        case "gt":
                            filterExpression = Expression.GreaterThan(
                                GetFilteredProperty(filter, paramExp),
                                GetFilteredValue(filter, paramExp)
                            );
                            break;
                    }
                }
                catch (Exception ex)
                {
                    throw new ArgumentException("Property: " + propertyToFilterBy.Name + ", " + ex.Message,
                        ex.InnerException);
                }
            }

            // If no expression generated then throw exception
            if (filter.Operator != "nested" && filterExpression == null)
                throw new ArgumentException(
                    $"Filter operator: {filter.Operator} for type: {filter.Value.GetType()} is not implemented (property: {propertyToFilterBy.Name} should be {propertyToFilterBy.PropertyType})");



            //check if there are nested filters and process them the same way as itself!
            if (filter.NestedFilters?.Count > 0)
            {
                foreach (var nestedFilter in filter.NestedFilters)
                {
                    var nested = GetFilterExpression(nestedFilter, targetType, paramExp);
                    if (filterExpression == null && filter.Operator == "nested")
                    {
                        //a nested filter is a way of specifying a nested filter that gets glued to containing property 
                        //according to the AndJoin value and internally looks like this
                        //( filter1 and/orelse filter2) //and or else depends on the value of AndJoin set on the nested filter type
                        filterExpression = nested;

                        continue;
                    }
                    //else - this should have thrown just before we examined the nested filters property

                    //nothing to join!
                    //this is a rare scenario when someone declared a nested filter but it was of 'nested' type and 
                    //had an empty nestedFilters array. in this case, no filter expression could be created
                    if(nested == null)
                        continue;

                    filterExpression = 
                        filter.AndJoin
                            ? Expression.AndAlso(filterExpression, nested)
                            : Expression.OrElse(filterExpression, nested);
                }
            }

            return filterExpression;
        }

        /// <summary>
        /// gets an expression that specifies a property to be tested
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paramExp"></param>
        /// <returns></returns>
        private static Expression GetFilteredProperty(ReadFilter filter, ParameterExpression paramExp)
        {
            return WrapIntoDbFunction(
                filter,
                Expression.Property(paramExp, filter.Property)
            );
        }

        /// <summary>
        /// Gets an expression that specifies the value to be tested against
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paramExp"></param>
        /// <param name="value"></param>
        /// <param name="forceValue">Whether or not should force value or try to use filter value if null</param>
        /// <returns></returns>
        private static Expression GetFilteredValue(ReadFilter filter, ParameterExpression paramExp,
            object value = null, bool forceValue = false)
        {
            return WrapIntoDbFunction(
                filter,
                Expression.Convert(
                    Expression.Constant(forceValue ? value : value ?? filter.Value),
                    Expression.Property(paramExp, filter.Property).Type
                )
            );
        }

        /// <summary>
        /// Wraps an expression into a single param db fn
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private static Expression WrapIntoDbFunction(ReadFilter filter, Expression e)
        {
            if (!string.IsNullOrEmpty(filter.DbFn))
            {
                return Expression.Call(typeof(DbFunctions), filter.DbFn, Type.EmptyTypes, e);
            }
            return e;
        }
    }
}
