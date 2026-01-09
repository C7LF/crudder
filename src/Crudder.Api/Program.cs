using Crudder.Application.Auth.Commands.RegisterUser;
using Crudder.Application.Todos.Commands.CreateTodo;
using Crudder.Domain.Interfaces;
using Crudder.Infrastructure.Persistence;
using Crudder.Infrastructure.Repositories;
using Crudder.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Crudder.Application.Common.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------
// Controllers & Swagger
// ---------------------------
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ---------------------------
// Database
// ---------------------------
builder.Services.AddDbContext<CrudderDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("TodosDb"))
);

// ---------------------------
// Repositories
// ---------------------------
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddScoped<ILabelRepository, LabelRepository>();

// ---------------------------
// Services
// ---------------------------
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasherService>();
// ---------------------------
// MediatR / CQRS
// ---------------------------
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(
        typeof(CreateTodoCommand).Assembly,       // Todos
        typeof(RegisterUserHandler).Assembly      // Auth
                                                  // add other feature assemblies here if needed
    );
});

// ---------------------------
// Authentication
// ---------------------------
builder.Services.AddJwtAuthentication(builder.Configuration);

// ---------------------------
// Build app
// ---------------------------
var app = builder.Build();

// ---------------------------
// Middleware
// ---------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exception = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>()?.Error;

        if (exception is UnauthorizedAccessException)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(new { error = exception.Message });
            return;
        }

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
    });
});

// ---------------------------
// Database Migrations
// ---------------------------
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CrudderDbContext>();
    db.Database.Migrate();
}

// ---------------------------
// Map Controllers
// ---------------------------
app.MapControllers();

// ---------------------------
// Run
// ---------------------------
app.Run();
