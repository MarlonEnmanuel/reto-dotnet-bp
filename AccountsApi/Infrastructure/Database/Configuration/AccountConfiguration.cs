using AccountsApi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountsApi.Infrastructure.Database.Configuration
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(a => a.Number);

            builder.Property(a => a.Number)
                   .HasMaxLength(20);

            builder.Property(a => a.Balance)
                   .HasPrecision(10, 2);

            builder.HasIndex(a => a.ClientId);
        }
    }
}
