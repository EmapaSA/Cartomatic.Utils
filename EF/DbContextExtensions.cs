﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cartomatic.Utils.EF
{
    public static class DbContextExtensions
    {

        private static Dictionary<System.Data.Entity.DbContext, Dictionary<Type, EntitySetMapping>> MappingsCache = new Dictionary<System.Data.Entity.DbContext, Dictionary<Type, EntitySetMapping>>();


        /// <summary>
        /// Gets the entity table name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbCtx"></param>
        /// <returns></returns>
        public static string GetTableName<T>(this System.Data.Entity.DbContext dbCtx)
        {
            var mappings = dbCtx.GetMappings<T>();

            // Find the storage entity set (table) that the entity is mapped
            var table = mappings
                .EntityTypeMappings.Single()
                .Fragments.Single()
                .StoreEntitySet;

            //Return the table name from the storage entity set
            return (string)table.MetadataProperties["Table"].Value ?? table.Name;
        }

        /// <summary>
        /// Gets table schema
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbCtx"></param>
        /// <returns></returns>
        public static string GetTableSchema<T>(this System.Data.Entity.DbContext dbCtx)
        {
            var mappings = dbCtx.GetMappings<T>();

            // Find the storage entity set (table) that the entity is mapped
            var table = mappings
                .EntityTypeMappings.Single()
                .Fragments.Single()
                .StoreEntitySet;

            // Return the table name from the storage entity set
            return (string)table.MetadataProperties["Schema"].Value ?? table.Schema;
        }

        /// <summary>
        /// Gets a mapped column name
        /// Based on https://romiller.com/2015/08/05/ef6-1-get-mapping-between-properties-and-columns/
        /// </summary>
        /// <param name="dbCtx"></param>
        /// <returns></returns>
        public static string GetTableColumnName<T>(this System.Data.Entity.DbContext dbCtx, string propertyName)
        {
            var mappings = dbCtx.GetMappings<T>();

            // Find the storage property (column) that the property is mapped
            var columnName = mappings
                .EntityTypeMappings.Single()
                .Fragments.Single()
                .PropertyMappings
                .OfType<ScalarPropertyMapping>()
                .Single(m => m.Property.Name == propertyName)
                .Column
                .Name;

            return columnName;
        }

        /// <summary>
        /// Gets the entity mapping for specified db ctx;
        /// based on https://romiller.com/2014/04/08/ef6-1-mapping-between-types-tables/
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbCtx"></param>
        /// <returns></returns>
        public static EntitySetMapping GetMappings<T>(this System.Data.Entity.DbContext dbCtx)
        {
            var type = typeof(T);

            //do a cache loolup first
            if (MappingsCache.ContainsKey(dbCtx) && MappingsCache[dbCtx] != null && MappingsCache[dbCtx].ContainsKey(type))
            {
                return MappingsCache[dbCtx][type];
            }

            var metadata = ((IObjectContextAdapter)dbCtx).ObjectContext.MetadataWorkspace;

            // Get the part of the model that contains info about the actual CLR types
            var objectItemCollection = ((ObjectItemCollection)metadata.GetItemCollection(DataSpace.OSpace));

            // Get the entity type from the model that maps to the CLR type
            var entityType = metadata
                    .GetItems<EntityType>(DataSpace.OSpace)
                    .Single(e => objectItemCollection.GetClrType(e) == type);

            // Get the entity set that uses this entity type
            var entitySet = metadata
                .GetItems<EntityContainer>(DataSpace.CSpace)
                .Single()
                .EntitySets
                .Single(s => s.ElementType.Name == entityType.Name);

            // Find the mapping between conceptual and storage model for this entity set
            var mapping = metadata.GetItems<EntityContainerMapping>(DataSpace.CSSpace)
                    .Single()
                    .EntitySetMappings
                    .Single(s => s.EntitySet == entitySet);

            //cache the results
            if (!MappingsCache.ContainsKey(dbCtx))
            {
                MappingsCache[dbCtx] = new Dictionary<Type, EntitySetMapping>();
            }
            MappingsCache[dbCtx].Add(type, mapping);

            return mapping;
        }

        /*
A Tweak for Advanced Mappings
EF supports an advanced mapping pattern called ‘Entity Splitting’. In this pattern you can split the properties of an entity between multiple tables. Here is an example of a Fluent API call that entity splits the Post class.

modelBuilder.Entity<Post>()
    .Map(m =>
    {
        m.Properties(p => new { p.PostId, p.Title, p.BlogId });
        m.ToTable("t_post");
    })
    .Map(m =>
    {
        m.Properties(p => new { p.PostId, p.Body });
        m.ToTable("t_post_body");
    });
To handle this we can update the GetTableName method to return an enumerable of the tables that the type maps to. The only changes to the previous implementation are the last two code blocks that find the table name from the mapping fragment.

public static IEnumerable<string> GetTableName(Type type, DbContext context)
{
    var metadata = ((IObjectContextAdapter)context).ObjectContext.MetadataWorkspace;
 
    // Get the part of the model that contains info about the actual CLR types
    var objectItemCollection = ((ObjectItemCollection)metadata.GetItemCollection(DataSpace.OSpace));
 
    // Get the entity type from the model that maps to the CLR type
    var entityType = metadata
            .GetItems<EntityType>(DataSpace.OSpace)
            .Single(e => objectItemCollection.GetClrType(e) == type);
 
    // Get the entity set that uses this entity type
    var entitySet = metadata
        .GetItems<EntityContainer>(DataSpace.CSpace)
        .Single()
        .EntitySets
        .Single(s => s.ElementType.Name == entityType.Name);
 
    // Find the mapping between conceptual and storage model for this entity set
    var mapping = metadata.GetItems<EntityContainerMapping>(DataSpace.CSSpace)
            .Single()
            .EntitySetMappings
            .Single(s => s.EntitySet == entitySet);
 
    // Find the storage entity sets (tables) that the entity is mapped
    var tables = mapping
        .EntityTypeMappings.Single()
        .Fragments;
 
    // Return the table name from the storage entity set
    return tables.Select(f => (string)f.StoreEntitySet.MetadataProperties["Table"].Value ?? f.StoreEntitySet.Name);
}
         */
    }
}
