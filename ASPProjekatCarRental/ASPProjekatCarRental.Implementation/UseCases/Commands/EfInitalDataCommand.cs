using ASPProjekatCarRental.Application.UseCases.Commands;
using ASPProjekatCarRental.Application.UseCases.DTO.ReceiveDto;
using ASPProjekatCarRental.DataAccess;
using ASPProjekatCarRental.Domain;
using Bogus;
using Bogus.Bson;
using Bogus.Extensions.UnitedKingdom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Bogus.Extensions.UnitedKingdom;

namespace ASPProjekatCarRental.Implementation.UseCases.Commands
{
    public class EfInitalDataCommand : EfUseCase, ICreateIntialDataCommand
    {
        public EfInitalDataCommand(CarRentalContext context) : base(context)
        {
        }

        public int Id => 1;

        public string Name => "Create Inital Data";

        public string Description => "Nesto";

        public void Execute(int request)
        {

            Random random = new Random();

            var usersFaker = new Faker<User>()
                            .RuleFor(x => x.FirstName, f => f.Name.FirstName())
                            .RuleFor(x => x.LastName, f => f.Name.LastName())
                            .RuleFor(x => x.UserName, (y, x) => y.Internet.UserName(x.FirstName, x.LastName))
                            .RuleFor(x => x.Email, (y, x) => y.Internet.Email(x.FirstName, x.LastName))
                            .RuleFor(x => x.Password, f => f.Internet.Password())
                            .RuleFor(x => x.Phone, f => f.Phone.PhoneNumberFormat())
                            .RuleFor(x => x.Adress, f => f.Address.FullAddress())
                            .RuleFor(x => x.ImagePath, f => f.Image.PicsumUrl());
            
            var users = usersFaker.Generate(request);

            /*var categoryCarsFaker = new Faker<Category>()
                                        .RuleFor(x => x.CategoryName, f => f.Vehicle.Type());
            var categoryCars = categoryCarsFaker.Generate(3);*/

            var categories = new List<Category>
            {
                new Category
                {
                    CategoryName = "SUV"
                },
                new Category
                {
                    CategoryName = "Hatchback"
                },
                new Category
                {
                    CategoryName = "Crossover"
                },
                new Category
                {
                    CategoryName = "Convertible"
                },
                new Category
                {
                    CategoryName = "Sedan"
                },
                new Category
                {
                    CategoryName = "Sports Car"
                },
                new Category
                {
                    CategoryName = "Coupe"
                },
                new Category
                {
                    CategoryName = "Station Wagon"
                },
                new Category
                {
                    CategoryName = "Minivan"
                },
                new Category
                {
                    CategoryName = "Pickup"
                }
            };

            

            var registrationPlates = new List<RegistrationPlate>();

            for(int i=0; i< 100;  i++)
            { 
                registrationPlates.Add(new RegistrationPlate
                    {
                        Plate = "BG" + "-"  + random.Next(10, 999) + "-" + "AC"
                    }
                );
            }

           
            var discounts = new List<Discount>
            {
                new Discount
                {
                    Category = categories.ElementAt(0),
                    Percentage = random.Next(5,10),
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddMonths(1),
                },
                new Discount
                {
                    Category = categories.ElementAt(1),
                    Percentage = random.Next(5,30),
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddMonths(2),
                },
                new Discount
                {
                    Category = categories.ElementAt(2),
                    Percentage = random.Next(5,25),
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddMonths(3),
                },
                new Discount
                {
                    Category = categories.ElementAt(3),
                    Percentage = random.Next(5,15),
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddMonths(2),
                }
            };

            var manufacturerFaker = new Faker<Manufacturer>()
                                        .RuleFor(x => x.Manufacturer_Name, f => f.Vehicle.Manufacturer());

            var manufacturer = manufacturerFaker.Generate(20);

            var modelFaker = new Faker<Model>()
                                 .RuleFor(x => x.ModelName, f => f.Vehicle.Model())
                                 .RuleFor(x => x.Manufacturer, f => manufacturer.ElementAt(random.Next(1, 20)));

            var model = modelFaker.Generate(30);

            var rentingPlaceFaker = new Faker<RentingPlace>()
                                        .RuleFor(x => x.Name, f => f.Company.CompanyName())
                                        .RuleFor(x => x.Adress, f => f.Address.FullAddress())
                                        .RuleFor(x => x.Phone, f => f.Phone.PhoneNumberFormat());

            var rentingPlace = rentingPlaceFaker.Generate(10);

            var cars = new List<Car>();

            for(int i=0; i< 100; i++)
            {
                cars.Add(new Car
                {
                    Model = model.ElementAt(random.Next(1, 30)),
                    RentingPlace = rentingPlace.ElementAt(random.Next(1, 10)),
                    Category = categories.ElementAt(random.Next(1, 10)),
                    ImagePath = $"image/slika{i}.jpg"
                });
            }

            var pricesFaker = new Faker<Price>()
                                 .RuleFor(x => x.Car, f => cars.ElementAt(random.Next(1, 100)))
                                 .RuleFor(x => x.PricePerDay, f => random.Next(20, 99))
                                 .RuleFor(x => x.PricePerMonth, f => random.Next(150, 500));

            var prices = pricesFaker.Generate(request);

            var rentingsFaker = new Faker<Renting>()
                               .RuleFor(x => x.User, f => users.ElementAt(random.Next(1, 100)))
                               .RuleFor(x => x.Car, f => cars.ElementAt(random.Next(1, 100)))
                               .RuleFor(x => x.StartDate, f => f.Date.Past())
                               .RuleFor(x => x.EndtDate, f => f.Date.Soon())
                               .RuleFor(x => x.SumCost, f => random.Next(20, 1000))
                               .RuleFor(x => x.RentAdress, f => f.Address.FullAddress())
                               .RuleFor(x => x.IsPaid, f => f.Random.Bool());

            var rentings = rentingsFaker.Generate(request);

            var registrationFaker = new Faker<Registration>()
                                        .RuleFor(x => x.RegistrationPlate, f => registrationPlates.ElementAt(random.Next(1, 100)))
                                        .RuleFor(x => x.StartOfRegistration, f => f.Date.Past())
                                        .RuleFor(x => x.EndOfRegistration, (f, x) => x.StartOfRegistration.AddYears(1))
                                        .RuleFor(x => x.Car, f => cars.ElementAt(random.Next(1, 100)));
            
            var registration = registrationFaker.Generate(request);

            var specifications = new List<Specification>
            {
                new Specification
                {
                    SpecificationName = "Year"
                },
                new Specification
                {
                    SpecificationName = "Power"
                },
                new Specification
                {
                    SpecificationName = "Fuel"
                },
                new Specification
                {
                    SpecificationName = "Number of seats"
                },
                new Specification
                {
                    SpecificationName = "Climate"
                }
            };

            var specificationsValues = new List<SpecificationValue>
            {
                new SpecificationValue
                {
                    Value = "1300"
                },
                new SpecificationValue
                {
                    Value = "5"
                },
                new SpecificationValue
                {
                    Value = "4"
                },
                new SpecificationValue
                {
                    Value = "Yes"
                },
                new SpecificationValue
                {
                    Value = "No"
                },
                new SpecificationValue
                {
                    Value = "2148"
                },
                new SpecificationValue
                {
                    Value = "Diesel"
                },
                new SpecificationValue
                {
                    Value = "Gasoline"
                },
            };

            var specSpecValues = new List<SpecificationSpecificationValue>
            {
                new SpecificationSpecificationValue
                {
                    Specification = specifications.ElementAt(1),
                    SpecificationValue = specificationsValues.ElementAt(0)
                },
                new SpecificationSpecificationValue
                {
                    Specification = specifications.ElementAt(1),
                    SpecificationValue = specificationsValues.ElementAt(5)
                },
                new SpecificationSpecificationValue
                {
                    Specification = specifications.ElementAt(2),
                    SpecificationValue = specificationsValues.ElementAt(6)
                },
                new SpecificationSpecificationValue
                {
                    Specification = specifications.ElementAt(2),
                    SpecificationValue = specificationsValues.ElementAt(7)
                }
            };

            var specSpecCars = new List<SpecificationCar>
            {
                new SpecificationCar
                {
                    SSpecificationSpecificationValue = specSpecValues.ElementAt(0),
                    Car = cars.ElementAt(5)
                },
                new SpecificationCar
                {
                    SSpecificationSpecificationValue = specSpecValues.ElementAt(1),
                    Car = cars.ElementAt(5)
                },
                new SpecificationCar
                {
                    SSpecificationSpecificationValue = specSpecValues.ElementAt(0),
                    Car = cars.ElementAt(1)
                },
                new SpecificationCar
                {
                    SSpecificationSpecificationValue = specSpecValues.ElementAt(3),
                    Car = cars.ElementAt(15)
                },
                new SpecificationCar
                {
                    SSpecificationSpecificationValue = specSpecValues.ElementAt(2),
                    Car = cars.ElementAt(10)
                },
                new SpecificationCar
                {
                    SSpecificationSpecificationValue = specSpecValues.ElementAt(3),
                    Car = cars.ElementAt(55)
                }
            };

            Context.Users.AddRange(users);
            Context.Manufacturers.AddRange(manufacturer);
            Context.Models.AddRange(model);
            Context.Categories.AddRange(categories);
            Context.Discounts.AddRange(discounts);
            Context.RegistrationPlates.AddRange(registrationPlates);
            Context.RentingPlaces.AddRange(rentingPlace);
            Context.Cars.AddRange(cars);
            Context.Registrations.AddRange(registration);
            Context.Rentings.AddRange(rentings);
            Context.Specifications.AddRange(specifications);
            Context.SpecificationValues.AddRange(specificationsValues);
            Context.SpecificationSpecificationValues.AddRange(specSpecValues);
            Context.SpecificationCars.AddRange(specSpecCars);

            Context.SaveChanges();
        }
    }
}
