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
    public class SpecSpecValueConfiguration : BaseEnityConfiguration<SpecificationSpecificationValue>
    {
        public override void ConfigureYourEnity(EntityTypeBuilder<SpecificationSpecificationValue> builder)
        {

            builder.HasMany(x => x.SpecificationCars)
                   .WithOne(x => x.SSpecificationSpecificationValue)
                   .HasForeignKey(x => x.SSpecificationValueId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
