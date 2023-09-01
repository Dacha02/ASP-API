using ASPProjekatCarRental.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.DataAccess
{
    public class CarRentalContext : DbContext
    {
        public CarRentalContext(DbContextOptions options = null) : base(options)
        {
        }

        public IApplicationUser User { get; }
        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.\SQLEXPRESS;Initial Catalog=CarRental;Integrated Security=True");
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            foreach (var entry in this.ChangeTracker.Entries())
            {
                if (entry.Entity is Entity e)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            e.CreatedAt = DateTime.UtcNow;
                            break;
                        case EntityState.Modified:
                            e.UpdatedAt = DateTime.UtcNow;
                            break;
                    }
                }
            }

            return base.SaveChanges();
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<RentingPlace> RentingPlaces { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Renting> Rentings { get; set; }
        public DbSet<RegistrationPlate> RegistrationPlates { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Specification> Specifications { get; set; }
        public DbSet<SpecificationValue> SpecificationValues { get; set; }
        public DbSet<SpecificationSpecificationValue> SpecificationSpecificationValues { get; set; }
        public DbSet<SpecificationCar> SpecificationCars { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UseCase> UseCases { get; set; }
        public DbSet<UserUseCase> UserUseCases { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<LoginModel> LoginModels { get; set; }
    }
}
