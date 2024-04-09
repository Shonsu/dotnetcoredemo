using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebMVCDemo.Models
{

    public class AddressEntityTypeConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(a => a.PostalCode)
            .HasDefaultValue("00-000");
            builder.HasMany(a => a.Employees)
            .WithOne(e => e.Address)
            .HasForeignKey(e => e.AddressId);

        }
    }
}
