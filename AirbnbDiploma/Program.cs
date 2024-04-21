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
                    policy.AllowAnyOrigin();
                    policy.AllowAnyMethod();
                    policy.AllowAnyHeader();
                });
        });

        // Services
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<ITokenService, TokenService>();
        builder.Services.AddScoped<IStayService, StayService>();
        builder.Services.AddScoped<IReviewsService, ReviewsService>();
        builder.Services.AddScoped<IUserService, UserService>();

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

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

        app.MapControllers();

        app.Run();
    }
}