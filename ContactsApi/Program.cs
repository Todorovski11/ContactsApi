using ContactsApi.Infrastructure.Persistence;
using ContactsApi.Infrastructure.Repositories;
using ContactsApi.Application.Interfaces;
using ContactsApi.Application.Commands;
using ContactsApi.Application.Queries;
using ContactsApi.Application.Validators;
using ContactsApi.Application.Pipeline;
using MediatR;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

using Application.Interfaces;
using ContactsApi.ContactsApi.Routes;
using Microsoft.OpenApi.Models;
using ContactsApi.Application.Commands.Companies;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Contacts API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter '<your_token>'"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
builder.Services.AddDbContext<ContactsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreateCompanyCommand).Assembly));
builder.Services.AddValidatorsFromAssemblyContaining<ContactValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CompanyValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CountryValidator>();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));


var jwtKey = "a-string-secret-that-is-longer-than-256-bits!";
var keyBytes = Encoding.UTF8.GetBytes(jwtKey);
var securityKey = new SymmetricSecurityKey(keyBytes);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = securityKey,
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                if (context.Request.Headers.ContainsKey("Authorization") &&
                    context.Request.Headers["Authorization"].ToString().StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                {
                    context.Token = context.Request.Headers["Authorization"].ToString().Substring("Bearer ".Length).Trim();
                }
                return Task.CompletedTask;
            },
            OnAuthenticationFailed = context =>
            {
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});


var app = builder.Build();

//// Apply Migrations 
//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<ContactsDbContext>();
//    dbContext.Database.Migrate();
//}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contacts API v1");
        c.RoutePrefix = string.Empty;
    });
}


app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.RegisterCompanyEndpoints();
app.RegisterContactEndpoints();
app.RegisterCountryEndpoints();
app.RegisterAuthEndpoints();

app.Run();
