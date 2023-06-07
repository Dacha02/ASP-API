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
    public class SpecificationValueConfiguration : BaseEnityConfiguration<SpecificationValue>
    {
        public override void ConfigureYourEnity(EntityTypeBuilder<SpecificationValue> builder)
        {
            builder.Property(x => x.Value).IsRequired().HasMaxLength(50);

            builder.HasIndex(x => x.Value).IsUnique();

            builder.HasMany(x => x.SpecificationValues)
                   .WithOne(x => x.SpecificationValue)
                   .HasForeignKey(x => x.SpecificationValueId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
