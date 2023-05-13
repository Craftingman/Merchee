using Merchee.BusinessLogic.Models;
using Merchee.DataAccess;
using Merchee.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

// Add services to the container.
var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddDbContext<CompanyDbContext>(optionsBuilder =>
        optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("EnterpriceManagerDb"))
    );

    builder.Services.Configure<IdentityOptions>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 6;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
    });

    builder.Services.AddIdentityCore<User>()
        .AddRoles<IdentityRole<Guid>>()
        .AddDefaultTokenProviders()
        .AddEntityFrameworkStores<CompanyDbContext>();

    // Add Authentication  
    builder.Services
        .AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = builder.Configuration["Jwt:Audience"],
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]))
            };
        });

    //Configure CORS
    builder.Services.AddCors(opt =>
    {
        opt.AddDefaultPolicy(policyBuilder =>
            policyBuilder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowAnyOrigin());
    });

    // Register services
    builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

    builder.Services.AddControllers().AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
    });

    builder.Services.AddHttpContextAccessor();
}

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
