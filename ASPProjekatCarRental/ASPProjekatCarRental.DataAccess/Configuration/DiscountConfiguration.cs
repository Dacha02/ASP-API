using ASPProjekatCarRental.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.DataAccess.Configuration
{
    public class DiscountConfiguration : BaseEnityConfiguration<Discount>
    {
        public override void ConfigureYourEnity(EntityTypeBuilder<Discount> builder)
        {
            builder.Property(x => x.Percentage).IsRequired();
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.EndDate).IsRequired();

        }
    }
}
