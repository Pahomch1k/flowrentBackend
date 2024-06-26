using AirbnbDiploma.DAL;
using AirbnbDiploma.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using AirbnbDiploma.DAL.UnitOfWork;
using AirbnbDiploma.BLL.Services.AuthService;
using AirbnbDiploma.BLL.Services.TokenService;
using AirbnbDiploma.Middlewares;
using AirbnbDiploma.Extensions.Service;
using AirbnbDiploma.Core.Entities;
using Microsoft.AspNetCore.Identity;
using AirbnbDiploma.BLL.Services.StaysService;
using AirbnbDiploma.BLL.Services.ReviewsService;
using AirbnbDiploma.BLL.Services.UserService;
using AirbnbDiploma.BLL.Services.EmailService;
using AirbnbDiploma.BLL.Services.WhishlistService;
using AirbnbDiploma.BLL.Services.BookingService;
using AirbnbDiploma.BLL.Services.ImageService;
using Prometheus;

namespace AirbnbDiploma;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration.Get("ConnectionStrings:MySql");
        var version = builder.Configuration.Get("ConnectionStrings:MySqlVersion");
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(connectionString, new MySqlServerVersion(version)));

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                policy =>
                {
                    policy.WithOrigins("http://localhost:4200", "http://flowrent.pp.ua", "https://flowrent.pp.ua", "http://84.235.174.123", "https://84.235.174.123");
                    policy.AllowAnyMethod();
                    policy.AllowAnyHeader();
                    policy.AllowCredentials();
                });
        });

        // Services
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<ITokenService, TokenService>();
        builder.Services.AddScoped<IStayService, StayService>();
        builder.Services.AddScoped<IReviewsService, ReviewsService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IEmailService, EmailService>();
        builder.Services.AddScoped<IWhishlistService, WhishlistService>();
        builder.Services.AddScoped<IBookingService, BookingService>();
        builder.Services.AddScoped<IImageService, ImageService>();

        // Auth
        builder.Services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders()
            .AddRoles<Role>();
        builder.Services.Configure<IdentityOptions>(options =>
        {
            options.User.RequireUniqueEmail = false;
        });
        builder.Services.ConfigureAuth(builder.Configuration);

        // Middlewares
        builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.ConfigureSwagger();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors();

        app.UseHttpMetrics();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

        app.MapControllers();
        app.MapMetrics();

        app.Run();
    }
}