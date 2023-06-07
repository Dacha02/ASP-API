using ASPProjekatCarRental.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.DataAccess.Configuration
{
    public class RegistrationConfiguration : BaseEnityConfiguration<Registration>
    {
        public override void ConfigureYourEnity(EntityTypeBuilder<Registration> builder)
        {
            builder.Property(x => x.StartOfRegistration).IsRequired();
            builder.Property(x => x.EndOfRegistration).IsRequired();

        }
    }
}
