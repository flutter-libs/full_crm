using System.Text;
using backend.Areas.Communication.Services;
using backend.Areas.Identity.Models;
using backend.Areas.Identity.Services;
using backend.Areas.Main.Services;
using backend.Areas.Utility.Services;
using backend.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace backend;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
        => Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(
                webBuilder => webBuilder.UseStartup<Startup>());
}

public class Startup
{
    public void ConfigureServices(IServiceCollection services, string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));;
        
             
        Log.Logger = new LoggerConfiguration()
            .WriteTo.File("/Areas/Communication/Logs/Communication_Logs.txt")
            .CreateLogger();
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("/Areas/Utility/Logs/ApplicationLogs.txt")
            .CreateLogger();
        builder.Host.UseSerilog();
        Log.Logger = new LoggerConfiguration()
            .WriteTo.MySQL(connectionString: connectionString, tableName: "Logs")
            .CreateLogger();
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Add Identity
        services.AddIdentityCore<User>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredUniqueChars = 0;
            })
            .AddRoles<Role>()
            .AddRoleManager<RoleManager<Role>>()
            .AddUserManager<UserManager<User>>()
            .AddSignInManager<SignInManager<User>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        var jwtSettings = builder.Configuration.GetSection("Jwt");
        var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.MetadataAddress = jwtSettings["MetadataAddress"]!;
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.Authority = jwtSettings["Authority"];
                options.Audience = jwtSettings["Audience"];
                
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });
        services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Identity/Login";
            options.Cookie.Name = "crm_cookie";
            options.Cookie.HttpOnly = true;
            options.LogoutPath = "/Identity/Logout";
            options.SlidingExpiration = true;
            options.AccessDeniedPath = "/Identity/AccessDenied";
            options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
        });
        services.Configure<PasswordHasherOptions>(option =>
        {
            option.IterationCount = 12000;
            option.CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV2;
        });
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", policy =>
            {
                policy.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                // (Optional: restrict origins in production)
            });
        });
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IEmailRepository, EmailRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IMessageUserRepository, MessageUsersRepository>();
        services.AddScoped<IContactRepository, ContactRepository>();
        services.AddScoped<IJobRepository, JobRepository>();
        services.AddScoped<ICampaignRepository, CampaignRepository>();
        services.AddScoped<IAnalyticRepository, AnalyticRepository>();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddControllers();
        services.AddSwaggerGen();
    }
    public void Configure(IApplicationBuilder ap, IWebHostEnvironment env, string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else
        {
            app.UseExceptionHandler("/Error");
        }
        app.UseCors("CorsPolicy");
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        
        app.MapControllers();
        app.Run();
    }
}

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        var connectionString = config.GetConnectionString("DefaultConnection");
        optionsBuilder.UseMySql(connectionString!, new MySqlServerVersion(new Version(8, 0, 25)));
        return new ApplicationDbContext(optionsBuilder.Options);
    }
}