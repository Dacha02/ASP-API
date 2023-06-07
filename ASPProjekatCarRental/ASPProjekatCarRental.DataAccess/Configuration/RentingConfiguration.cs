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
    public class RentingConfiguration : BaseEnityConfiguration<Renting>
    {
        public override void ConfigureYourEnity(EntityTypeBuilder<Renting> builder)
        {
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.EndtDate).IsRequired();
            builder.Property(x => x.SumCost).IsRequired().HasPrecision(18, 2);
            builder.Property(x => x.RentAdress).HasMaxLength(100);
            builder.Property(x => x.IsPaid).HasDefaultValue(false);
        }
    }
}
