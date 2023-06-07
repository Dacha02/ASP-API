using ASPProjekatCarRental.Api.Core;
using ASPProjekatCarRental.Api.Extensions;
using ASPProjekatCarRental.Application.Emails;
using ASPProjekatCarRental.Application.Logging;
using ASPProjekatCarRental.Application.UseCases;
using ASPProjekatCarRental.Application.UseCases.Commands;
using ASPProjekatCarRental.Implementation;
using ASPProjekatCarRental.Implementation.Emails;
using ASPProjekatCarRental.Implementation.Logging;
using ASPProjekatCarRental.Implementation.UseCases.Commands;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Konfiguracija DIC
var settings = new AppSettings();
builder.Configuration.Bind(settings);
builder.Services.AddSingleton(settings);

builder.Services.AddApplicationUser();
builder.Services.AddTransient<ITokenStorage, InMemoryTokenStorage>();
builder.Services.AddJwt(settings);

//Dodavanje contexta
builder.Services.AddCarRentalDbContex();

//Dodavanje UseCase-va
builder.Services.AddUseCases();

builder.Services.AddTransient<UseCaseHandler>();
builder.Services.AddTransient<IExceptionLogger, ConsoleExceptionLogger>();
builder.Services.AddTransient<IUseCaseLogger, ExecutedUseCasesLog>();
builder.Services.AddTransient < IEmailSender>(x =>
                    new SmtpEmailSender(settings.EmailOptions.FromEmail,
                                        settings.EmailOptions.Password,
                                        settings.EmailOptions.Port,
                                        settings.EmailOptions.Host));

builder.Services.AddHttpContextAccessor();

builder.Services.AddSwaggerGen(c =>
{
c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Car Rental API",
        Version = "v1",
        Description = "An API to perform Renting Cars operations",
        Contact = new OpenApiContact
        {
            Name = "David Stevic",
            Email = "david.stevic.58.20@ict.edu.rs",
            //Url = new Uri("https://twitter.com/jwalkner"),
        },
        License = new OpenApiLicense
        {
            Name = "Employee API LICX",
            Url = new Uri("https://example.com/license"),
        }
    });
        // Set the comments path for the Swagger JSON and UI.
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT Token with Bearer formatk like Bearer[space] token"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
        new OpenApiSecurityScheme
            {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id="Bearer"
            }
        },
        new string[]{ }
        }
    });
});
//Kraj kofiguracije

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionHandler>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
