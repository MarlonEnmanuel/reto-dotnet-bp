using ClientsApi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClientsApi.Infrastructure.Database.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                   .HasMaxLength(100);

            builder.Property(e => e.Identification)
                   .HasMaxLength(20);

            builder.Property(e => e.Address)
                   .HasMaxLength(200);

            builder.Property(e => e.PhoneNumber)
                   .HasMaxLength(20);

            builder.HasIndex(e => e.Identification)
                   .IsUnique();
        }
    }
}
