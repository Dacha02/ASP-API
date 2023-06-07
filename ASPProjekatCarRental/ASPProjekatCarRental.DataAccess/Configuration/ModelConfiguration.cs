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
    public class ModelConfiguration : BaseEnityConfiguration<Model>
    {
        public override void ConfigureYourEnity(EntityTypeBuilder<Model> builder)
        {
            builder.Property(x => x.ModelName).IsRequired().HasMaxLength(30);

            builder.HasIndex(x=> x.ModelName);

            builder.HasMany(x => x.Cars)
                   .WithOne(x => x.Model)
                   .HasForeignKey(x => x.ModelId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
