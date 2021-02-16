using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MGK.ServiceBase.DAL
{
	public abstract class CustomDbContext : DbContext
	{
        protected CustomDbContext(DbContextOptions options)
            : base(options)
        {
            ChangeTracker.AutoDetectChangesEnabled = true;
            ChangeTracker.CascadeDeleteTiming = CascadeTiming.OnSaveChanges;
            ChangeTracker.DeleteOrphansTiming = CascadeTiming.OnSaveChanges;
        }
    }
}
