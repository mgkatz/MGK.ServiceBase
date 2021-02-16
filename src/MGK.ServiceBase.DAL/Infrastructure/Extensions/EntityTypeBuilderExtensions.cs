using MGK.Acceptance;
using MGK.ServiceBase.DAL.Infrastructure.DataUnits;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MGK.ServiceBase.DAL.Infrastructure.Extensions
{
    public static class EntityTypeBuilderExtensions
    {
        public static void SetupBaseEntity<TEntity, TKey>(this EntityTypeBuilder<TEntity> builder)
            where TEntity : DataUnit<TKey>
        {
            Ensure.Parameter.IsNotNull(builder, nameof(builder));

            builder.Property(p => p.Id).ValueGeneratedNever();
        }
    }
}
