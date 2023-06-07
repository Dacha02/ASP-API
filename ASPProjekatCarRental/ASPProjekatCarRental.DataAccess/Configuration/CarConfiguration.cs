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
    public class CarConfiguration : BaseEnityConfiguration<Car>
    {
        public override void ConfigureYourEnity(EntityTypeBuilder<Car> builder)
        {
            builder.Property(x => x.ImagePath).HasMaxLength(150).IsRequired();

            builder.HasMany(x => x.Registrations)
                   .WithOne(x => x.Car)
                   .HasForeignKey(x => x.CarId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.SpecificationCar)
                   .WithOne(x => x.Car)
                   .HasForeignKey(x => x.CarId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Prices)
                   .WithOne(x => x.Car)
                   .HasForeignKey(x => x.CarId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Rentings)
                   .WithOne(x => x.Car)
                   .HasForeignKey(x => x.CarId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
