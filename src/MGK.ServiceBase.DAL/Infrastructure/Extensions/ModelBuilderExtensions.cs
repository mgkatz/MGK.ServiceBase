using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Linq;
using System.Reflection;

namespace MGK.ServiceBase.DAL.Infrastructure.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static ModelBuilder ApplyConventions(this ModelBuilder modelBuilder)
        {
            foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
            {
                // Use the entity name instead of the Context.DbSet<T> name
                // ref https://docs.microsoft.com/en-us/ef/core/modeling/relational/tables#conventions
                modelBuilder.Entity(entityType.ClrType).ToTable(entityType.ClrType.Name);
            }

            return modelBuilder;
        }

        public static ModelBuilder ApplyConfigurationsFromAssembly(
            this ModelBuilder modelBuilder,
            Assembly assembly,
            Type dbConfiguration)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                assembly,
                t => t.GetInterfaces()
                    .Any(i =>
                        i.IsGenericType &&
                        i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>) &&
                        dbConfiguration.IsAssignableFrom(i.GenericTypeArguments[0]))
            );

            return modelBuilder;
        }
    }
}
