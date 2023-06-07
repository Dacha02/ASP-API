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
    public class RegistrationPlateConfiguration : BaseEnityConfiguration<RegistrationPlate>
    {
        public override void ConfigureYourEnity(EntityTypeBuilder<RegistrationPlate> builder)
        {
            builder.Property(rp => rp.Plate).IsRequired().HasMaxLength(12);

            builder.HasIndex(x => x.Plate);

            builder.HasMany(x => x.Registrations)
                   .WithOne(x => x.RegistrationPlate)
                   .HasForeignKey(x => x.RegistrationPlateId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
