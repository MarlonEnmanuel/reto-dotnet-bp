using AccountsApi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountsApi.Infrastructure.Database.Configuration
{
    public class MovementConfiguration : IEntityTypeConfiguration<Movement>
    {
        public void Configure(EntityTypeBuilder<Movement> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.AccountNumber)
                   .HasMaxLength(20);

            builder.Property(m => m.Amount)
                   .HasPrecision(10, 2);

            builder.Property(m => m.Balance)
                   .HasPrecision(10, 2);

            builder.Property(m => m.Description)
                   .HasMaxLength(200);

            builder.HasIndex(m => m.DateTime);

            builder.HasOne(m => m.Account)
                   .WithMany(a => a.Movements)
                   .HasForeignKey(m => m.AccountNumber)
                   .IsRequired();
        }
    }
}
