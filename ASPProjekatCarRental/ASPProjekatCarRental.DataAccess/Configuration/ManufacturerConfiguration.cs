using ASPProjekatCarRental.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.DataAccess.Configuration
{
    public class ManufacturerConfiguration : BaseEnityConfiguration<Manufacturer>
    {
        public override void ConfigureYourEnity(EntityTypeBuilder<Manufacturer> builder)
        {
            builder.Property(x => x.Manufacturer_Name).IsRequired().HasMaxLength(30);

            builder.HasIndex(x => x.Manufacturer_Name);

            builder.HasMany(x => x.Models)
                   .WithOne(x => x.Manufacturer)
                   .HasForeignKey(x => x.ManufacturerId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
