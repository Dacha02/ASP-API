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
    public class AuditLogConfiguration : BaseEnityConfiguration<AuditLog>
    {
        public override void ConfigureYourEnity(EntityTypeBuilder<AuditLog> builder)
        {
            builder.Property(x => x.Data).HasDefaultValue(250);
            builder.Property(x => x.IsAuthorized).HasDefaultValue(false);
            builder.Property(x => x.UseCaseName).HasDefaultValue(150);
            builder.Property(x => x.TimeOfExecution).HasDefaultValueSql("GETDATE()");

            builder.HasIndex(x => x.UseCaseName);

        }
    }
}
