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
    public class SpecificationConfiguration : BaseEnityConfiguration<Specification>
    {
        public override void ConfigureYourEnity(EntityTypeBuilder<Specification> builder)
        {
            builder.Property(x => x.SpecificationName).IsRequired().HasMaxLength(50);

            builder.HasIndex(x => x.SpecificationName).IsUnique();

            builder.HasMany(x => x.SpecificationValues)
                   .WithOne(x => x.Specification)
                   .HasForeignKey(x => x.SpecificationId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
