using System.Text;
using CrudderApi.Data;
using CrudderApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

public static class ServiceExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<AuthService>();
        services.AddScoped<TodoService>();
        services.AddScoped<UserService>();
        services.AddScoped<LabelService>();
        services.AddAutoMapper(typeof(Program));
    }

    public static void AddDatabase(this IServiceCollection services, IConfiguration config, IWebHostEnvironment env)
    {
        services.AddDbContext<TodoContext>(opt =>
        {
            opt.UseNpgsql(config.GetConnectionString("TodosDb"));
            if (env.IsDevelopment())
            {
                opt.EnableSensitiveDataLogging();
                opt.EnableDetailedErrors();
            }
        });
    }

    public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration config)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var token = context.Request.Cookies["jwt"];
                        if (!string.IsNullOrEmpty(token))
                            context.Token = token;
                        return Task.CompletedTask;
                    }
                };

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(config["Jwt:Key"]
                            ?? throw new Exception("JWT Key missing")))
                };
            });
    }
}