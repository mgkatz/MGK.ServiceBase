using MGK.Acceptance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace MGK.ServiceBase.DAL.Infrastructure.Factories
{
    public abstract class CustomDbContextDesignFactory<TContext> : IDesignTimeDbContextFactory<TContext>
        where TContext : DbContext
    {
        protected abstract string AppSettingsFile { get; }

        protected abstract string DbName { get; }

        public TContext CreateDbContext(string[] args)
        {
            Ensure.Value.IsNotNullNorEmptyNorWhiteSpace(AppSettingsFile, DALResources.MessagesResources.ErrorAppSettingsFileNotDefined);
            Ensure.Value.IsNotNullNorEmptyNorWhiteSpace(DbName, DALResources.MessagesResources.ErrorContextName);

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(AppSettingsFile)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<TContext>();
            var connectionString = configuration.GetConnectionString(DbName);

            optionsBuilder.UseSqlServer(connectionString);
            return (TContext)Activator.CreateInstance(typeof(TContext), optionsBuilder.Options);
        }
    }
}
