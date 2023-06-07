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
    public class RentingPlaceConfiguration : BaseEnityConfiguration<RentingPlace>
    {
        public override void ConfigureYourEnity(EntityTypeBuilder<RentingPlace> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Adress).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Phone).IsRequired().HasMaxLength(20);

            builder.HasIndex(x=> x.Name);

            builder.HasMany(x => x.Cars)
                   .WithOne(x => x.RentingPlace)
                   .HasForeignKey(x => x.RentPlaceId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
