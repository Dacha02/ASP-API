using ASPProjekatCarRental.Application.UseCases.Commands;
using ASPProjekatCarRental.Application.UseCases.DTO.ReceiveDto;
using ASPProjekatCarRental.DataAccess;
using ASPProjekatCarRental.Domain;
using ASPProjekatCarRental.Implementation;
using Bogus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASPProjekatCarRental.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InitialController : ControllerBase
    {
        private CarRentalContext Context;

        public InitialController(CarRentalContext contex)
        {
            Context = contex;
        }

        /// <summary>
        /// Inserting intial data
        /// </summary>
        /// <remarks>
        /// This methos is inserting some random values to all tables
        /// </remarks>  
        /// <response code="201">Successfully inserted data</response>
        /// <response code="500">Server Error</response>

        // POST api/<InitialController>
        [HttpPost]
        public IActionResult Post()
        {
            try
            {
                Random random = new Random();

                var users = new List<User>
                {
                    new User
                    {
                        FirstName = "Anonymous",
                        LastName = "Anonimusic",
                        UserName = "Annonymous",
                        Email = "annymous@asp-api.com",
                        Password = BCrypt.Net.BCrypt.HashPassword("Sifra123!"),
                        Phone = "0612345678",
                        Adress = "Brace Grim 15",
                        ImagePath = "slika1.png"
                    },
                    new User
                    {
                        FirstName = "Admin",
                        LastName = "Admirovic",
                        UserName = "Admin",
                        Email = "admin@gmail.com",
                        Password = BCrypt.Net.BCrypt.HashPassword("Sifra123!"),
                        Phone = "06987456123",
                        Adress = "Brace grim 15",
                        ImagePath = "slika2.png"
                    },
                };

                var useCases = new List<UseCase>
                {
                    new UseCase
                    {
                        UseCaseName = "Inicijalno kreiranje podataka"
                    },
                    new UseCase
                    {
                        UseCaseName = "Register User Use Case"
                    },
                    new UseCase
                    {
                        UseCaseName = "Get Cars Query"
                    },
                    new UseCase
                    {
                        UseCaseName = "Insert Prices Command"
                    },
                    new UseCase
                    {
                        UseCaseName = "Insert Rent Command"
                    },
                    new UseCase
                    {
                        UseCaseName = "Insert Car Command"
                    },
                    new UseCase
                    {
                        UseCaseName = "Delete Specification Command"
                    },
                    new UseCase
                    {
                        UseCaseName = "Change Discount Command"
                    },
                    new UseCase
                    {
                        UseCaseName = "Get Audit Logs Query"
                    },
                    new UseCase
                    {
                        UseCaseName = "Delete Car Command"
                    },
                    new UseCase
                    {
                        UseCaseName = "Find Specific Car Query"
                    },
                    new UseCase
                    {
                        UseCaseName = "Change Car Command"
                    },
                    new UseCase
                    {
                        UseCaseName = "Get Users Query"
                    },
                    new UseCase
                    {
                        UseCaseName = "Delete Rent Command"
                    },
                    new UseCase
                    {
                        UseCaseName = "Delete User Command"
                    },
                    new UseCase
                    {
                        UseCaseName = "Update User Command"
                    },
                    new UseCase
                    {
                        UseCaseName = "Find User Rentings Query"
                    },
                    new UseCase
                    {
                        UseCaseName = "Get car details Query"
                    },
                    new UseCase
                    {
                        UseCaseName = "Activate User Command"
                    },
                    new UseCase
                    {
                        UseCaseName = "Find User Query"
                    },
                };

                var userUseCases = new List<UserUseCase>
                {
                    new UserUseCase
                    {
                        User = users.ElementAt(0),
                        UseCase = useCases.ElementAt(1)
                    }
                };

                for(int i=2; i< useCases.Count; i++)
                {
                    var newUserUsecas =
                        new UserUseCase
                        {
                            User = users.ElementAt(1),
                            UseCase = useCases.ElementAt(i)
                        };

                    userUseCases.Add(newUserUsecas);
                }

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

                for (int i = 0; i < 100; i++)
                {
                    registrationPlates.Add(new RegistrationPlate
                    {
                        Plate = "BG" + "-" + random.Next(10, 999) + "-" + "AC"
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
                                     .RuleFor(x => x.Manufacturer, f => manufacturer.ElementAt(random.Next(1, 19)));

                var model = modelFaker.Generate(30);

                var rentingPlaceFaker = new Faker<RentingPlace>()
                                            .RuleFor(x => x.Name, f => f.Company.CompanyName())
                                            .RuleFor(x => x.Adress, f => f.Address.FullAddress())
                                            .RuleFor(x => x.Phone, f => f.Phone.PhoneNumberFormat());

                var rentingPlace = rentingPlaceFaker.Generate(10);

                var cars = new List<Car>();

                for (int i = 0; i <= 50; i++)
                {
                    cars.Add(new Car
                    {
                        Model = model.ElementAt(random.Next(1, 29)),
                        RentingPlace = rentingPlace.ElementAt(random.Next(1, 9)),
                        Category = categories.ElementAt(random.Next(1, 9)),
                        ImagePath = $"image/slika{i}.jpg"
                    });
                }

                var pricesFaker = new Faker<Price>()
                                     .RuleFor(x => x.Car, f => cars.ElementAt(random.Next(1, 49)))
                                     .RuleFor(x => x.PricePerDay, f => random.Next(20, 99))
                                     .RuleFor(x => x.PricePerMonth, f => random.Next(150, 500));

                var prices = pricesFaker.Generate(10);

                /*var rentingsFaker = new Faker<Renting>()
                                   .RuleFor(x => x.User, f => users.ElementAt(random.Next(1, 100)))
                                   .RuleFor(x => x.Car, f => cars.ElementAt(random.Next(1, 100)))
                                   .RuleFor(x => x.StartDate, f => f.Date.Past())
                                   .RuleFor(x => x.EndtDate, f => f.Date.Soon())
                                   .RuleFor(x => x.SumCost, f => random.Next(20, 1000))
                                   .RuleFor(x => x.RentAdress, f => f.Address.FullAddress())
                                   .RuleFor(x => x.IsPaid, f => f.Random.Bool());

                var rentings = rentingsFaker.Generate(100);*/

                var registrationFaker = new Faker<Registration>()
                                            .RuleFor(x => x.RegistrationPlate, f => registrationPlates.ElementAt(random.Next(1, 99)))
                                            .RuleFor(x => x.StartOfRegistration, f => f.Date.Past())
                                            .RuleFor(x => x.EndOfRegistration, (f, x) => x.StartOfRegistration.AddYears(1))
                                            .RuleFor(x => x.Car, f => cars.ElementAt(random.Next(1, 49)));

                var registration = registrationFaker.Generate(10);

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
                    Car = cars.ElementAt(45)
                }
            };

                Context.Users.AddRange(users);
                Context.UseCases.AddRange(useCases);
                Context.UserUseCases.AddRange(userUseCases);
                Context.Manufacturers.AddRange(manufacturer);
                Context.Models.AddRange(model);
                Context.Categories.AddRange(categories);
                Context.Discounts.AddRange(discounts);
                Context.RegistrationPlates.AddRange(registrationPlates);
                Context.RentingPlaces.AddRange(rentingPlace);
                Context.Prices.AddRange(prices);
                Context.Cars.AddRange(cars);
                Context.Registrations.AddRange(registration);
                //Context.Rentings.AddRange(rentings);
                Context.Specifications.AddRange(specifications);
                Context.SpecificationValues.AddRange(specificationsValues);
                Context.SpecificationSpecificationValues.AddRange(specSpecValues);
                Context.SpecificationCars.AddRange(specSpecCars);

                Context.SaveChanges();

                return StatusCode(201, new { message = "Successfully inserted fake data" });
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return StatusCode(500, new { errorMessage });
            }

        }
    }
}
