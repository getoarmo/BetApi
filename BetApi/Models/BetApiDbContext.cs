using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BetApi.Models
{
    public class BetApiDbContext : DbContext
    {
        public BetApiDbContext(DbContextOptions<BetApiDbContext> options) : base(options) { }

        public DbSet<BetItem> BetItems { get; set; }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            var AddedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Added).ToList();

            AddedEntities.ForEach(E =>
            {
                E.Property("CreatedDate").CurrentValue = DateTime.Now;
            });

            var EditedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Modified).ToList();

            EditedEntities.ForEach(E =>
            {
                E.Property("ModifiedDate").CurrentValue = DateTime.Now;
                E.Property("IsUpdated").CurrentValue = true;
            });

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

    }
}
