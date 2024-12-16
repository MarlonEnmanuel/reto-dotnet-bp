using ClientsApi.Domain;
using ClientsApi.Infrastructure.Database.Configurations;
using Microsoft.EntityFrameworkCore;

namespace ClientsApi.Infrastructure.Database
{
    public class ClientsContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new ClientConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
