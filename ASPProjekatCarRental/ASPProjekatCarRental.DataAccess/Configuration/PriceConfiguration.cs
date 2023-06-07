using ASPProjekatCarRental.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.DataAccess.Configuration
{
    public class PriceConfiguration : BaseEnityConfiguration<Price>
    {
        public override void ConfigureYourEnity(EntityTypeBuilder<Price> builder)
        {
            builder.Property(x => x.PricePerDay).IsRequired().HasPrecision(18, 2);
            builder.Property(x=> x.PricePerMonth).IsRequired().HasPrecision(18, 2);

        }
    }
}
