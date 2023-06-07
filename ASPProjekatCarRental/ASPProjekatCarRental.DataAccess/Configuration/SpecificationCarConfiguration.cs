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
    public class SpecificationCarConfiguration : IEntityTypeConfiguration<SpecificationCar>
    {
        public void Configure(EntityTypeBuilder<SpecificationCar> builder)
        {
            builder.HasKey(x => new { x.SSpecificationValueId, x.CarId });
        }
    }
}
