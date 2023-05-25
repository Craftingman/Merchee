using Merchee.BLL.Abstractions;
using Merchee.BLL.Services;
using Merchee.BusinessLogic.Models;
using Merchee.DataAccess;
using Merchee.Domain.Entities;
using Merchee.WebAPI.Mapping;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

// Add services to the container.
var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddDbContext<CompanyDbContext>(optionsBuilder =>
        optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("EnterpriceManagerDb"))
    );

    builder.Services.AddAutoMapper(typeof(GeneralProfile));

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
    builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
    builder.Services.AddScoped<ICompanyService, CompanyService>();
    builder.Services.AddScoped<IProductService, ProductService>();
    builder.Services.AddScoped<IShelfService, ShelfService>();
    builder.Services.AddScoped<IShelfProductService, ShelfProductService>();
    builder.Services.AddScoped<IShelfItemService, ShelfItemService>();
    builder.Services.AddScoped<ICustomerShelfActionService, CustomerShelfActionService>();
    builder.Services.AddScoped<IReplenishmentRequestService, ReplenishmentRequestService>();

    builder.Services.AddControllers().AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
    });

    builder.Services.AddHttpContextAccessor();

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(option =>
    {
        option.SwaggerDoc("v1", new OpenApiInfo { Title = "Merchee API", Version = "v1" });
        option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter a valid token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        });

        option.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type=ReferenceType.SecurityScheme,
                        Id="Bearer"
                    }
                },
                new string[]{}
            }
        });
    });
}

var app = builder.Build();
{
    InitializeDatabase(app.Services);

    app.UseRouting();
    app.UseCors();
    app.UseAuthentication();
    app.UseAuthorization();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.MapControllers();

    app.Run();
}

static void InitializeDatabase(IServiceProvider services)
{
    using var serviceScope = services.CreateScope();

    var context = serviceScope.ServiceProvider.GetRequiredService<CompanyDbContext>();
    var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

    CompanyDbInitializer.SeedCompany(context);
    CompanyDbInitializer.SeedSuperAdmin(userManager, roleManager);
}