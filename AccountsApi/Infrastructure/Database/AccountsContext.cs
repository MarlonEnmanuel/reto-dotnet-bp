using AccountsApi.Domain;
using AccountsApi.Infrastructure.Database.Configuration;
using Microsoft.EntityFrameworkCore;

namespace AccountsApi.Infrastructure.Database
{
    public class AccountsContext(DbContextOptions<AccountsContext> options) : DbContext(options)
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Movement> Movements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new MovementConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
