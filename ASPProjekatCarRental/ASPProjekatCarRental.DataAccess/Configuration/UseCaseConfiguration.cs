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
    public class UseCaseConfiguration : BaseEnityConfiguration<UseCase>
    {
        public override void ConfigureYourEnity(EntityTypeBuilder<UseCase> builder)
        {
            builder.Property(x => x.UseCaseName).HasDefaultValue(150);

            builder.HasIndex(x=> x.UseCaseName).IsUnique();

            builder.HasMany(x => x.UserUseCase)
                   .WithOne(x => x.UseCase)
                   .HasForeignKey(x => x.UseCaseId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
