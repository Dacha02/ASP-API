using ASPProjekatCarRental.Api.Core;
using ASPProjekatCarRental.Application.UseCases.Commands;
using ASPProjekatCarRental.Application.UseCases.Queries;
using ASPProjekatCarRental.DataAccess;
using ASPProjekatCarRental.Domain;
using ASPProjekatCarRental.Implementation.UseCases.Commands;
using ASPProjekatCarRental.Implementation.UseCases.Queries;
using ASPProjekatCarRental.Implementation.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ASPProjekatCarRental.Api.Extensions
{
    public static class ContainerExtenstions
    {
        public static void AddJwt(this IServiceCollection services, AppSettings settings)
        {
            services.AddTransient(x =>
            {
                var context = x.GetService<CarRentalContext>();
                var settings = x.GetService<AppSettings>();
                var tokenStorage = x.GetService<ITokenStorage>();

                return new JwtManager(context, settings.JwtSettings, tokenStorage);
            });

            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = settings.JwtSettings.Issuer,
                    ValidateIssuer = true,
                    ValidAudience = "Any",
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.JwtSettings.SecretKey)),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                cfg.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        //Token dohvatamo iz Authorization header-a

                        var header = context.Request.Headers["Authorization"];

                        var token = header.ToString().Split("Bearer ")[1];

                        var handler = new JwtSecurityTokenHandler();

                        var tokenObj = handler.ReadJwtToken(token);

                        string jti = tokenObj.Claims.FirstOrDefault(x => x.Type == "jti").Value;


                        //ITokenStorage

                        ITokenStorage storage = context.HttpContext.RequestServices.GetService<ITokenStorage>();

                        bool isValid = storage.TokenExists(jti);

                        if (!isValid)
                        {
                            context.Fail("Token is not valid.");
                        }

                        return Task.CompletedTask;
                    }
                };

            });
        }

        public static void AddApplicationUser(this IServiceCollection services)
        {
            services.AddTransient<IApplicationUser>(x =>
            {
                var accessor = x.GetService<IHttpContextAccessor>();
                var header = accessor.HttpContext.Request.Headers["Authorization"];

                //Pristup payload-u
                var claims = accessor.HttpContext.User;

                if (claims == null || claims.FindFirst("UserId") == null)
                {
                    return new AnnonymousUser();
                }

                var actor = new JwtUser
                {
                    Email = claims.FindFirst("Email").Value,
                    Id = Int32.Parse(claims.FindFirst("UserId").Value),
                    Identity = claims.FindFirst("Email").Value,
                    UseCaseIds = JsonConvert.DeserializeObject<List<int>>(claims.FindFirst("UseCases").Value)
                };

                return actor;
            });
        }


        public static void AddUseCases(this IServiceCollection services)
        {

            services.AddTransient<ICreateIntialDataCommand, EfInitalDataCommand>();
            services.AddTransient<IRegisterUserCommand, EfRegisterUserCommand>();
            services.AddTransient<IGetCarsQuery, EfGetCarsQuery>();
            services.AddTransient<ICreatePriceDto, EfInsertPriceCommand>();
            services.AddTransient<ICreateRentCommand, EfCreateRentCommand>();
            services.AddTransient<ICreateCarCommand, EfCreateCarCommand>();
            services.AddTransient<IDeleteSpecificationCommand, EfDeleteSpecificationCommand>();
            services.AddTransient<IPutDiscountCommand, EfPutDiscountCommand>();
            services.AddTransient<IGetAuditLogs, EfGetAuditLogQuery>();
            services.AddTransient<IDeleteCarCommand, EfDeleteCarCommand>();
            services.AddTransient<IFindCarQuery, EfFindCarQuery>();
            services.AddTransient<IChangeCarCommand, EfChangeCarCommand>();
            services.AddTransient<IGetUsersQuery, EfGetUsersQuery>();
            services.AddTransient<IDeleteRentCommand, EfDeleteRentCommand>();
            services.AddTransient<IDeleteUserCommand, EfDeleteUserCommand>();
            services.AddTransient<IUpdateUserCommand, EfUpdateUserCommand>();
            services.AddTransient<IFindUserQuery, EfFindUserQuery>();
            services.AddTransient<IFindUserRentingsQuery, EfUserRentingsQuery>();
            services.AddTransient<IActivateUserCommand, EfActivateUserCommand>();
            services.AddTransient<IGetCarDetailsQuery, EfGetCarDetailsQuery>();
            services.AddTransient<IGetProfitsQuery, EfGetProfitsQuery>();
            services.AddTransient<IGetRentingsQuery, EfGetRentingsQuery>();
            services.AddTransient<IChangeIsPaidCommand, EfChangeIsPaidCommand>();

            #region Validators
            services.AddTransient<RegisterUserValidator>();
            services.AddTransient<InsertPricesValidator>();
            services.AddTransient<InsertRentValidator>();
            services.AddTransient<InsertCarValidator>();
            services.AddTransient<DeleteSpecificationValidator>();
            services.AddTransient<PutDiscountValidator>();
            services.AddTransient<CheckCarIdValidator>();
            services.AddTransient<ChangeCarValidator>();
            services.AddTransient<DeleteRentValidator>();
            services.AddTransient<DeleteUserValidator>();
            services.AddTransient<UpdateUserValidator>();
            #endregion
        }

        public static void AddCarRentalDbContex(this IServiceCollection services)
        {
            services.AddTransient(x =>
            {
                var optionsBuilder = new DbContextOptionsBuilder();

                var connString = x.GetService<AppSettings>().ConnString;

                optionsBuilder.UseSqlServer(connString).UseLazyLoadingProxies();

                var options = optionsBuilder.Options;

                return new CarRentalContext(options);
            });
        }
    }
}
