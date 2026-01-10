using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;


namespace Crudder.Infrastructure.Persistence;

public class CrudderDbContextFactory : IDesignTimeDbContextFactory<CrudderDbContext>
{
    public CrudderDbContext CreateDbContext(string[] args)
    {
        // Build configuration from appsettings.json
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) // usually the Infrastructure project root
            .AddJsonFile("appsettings.Development.json", optional: false)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<CrudderDbContext>();
        var connectionString = configuration.GetConnectionString("TodosDb") ?? throw new InvalidOperationException("Connection string 'TodosDb' not found.");
        optionsBuilder.UseNpgsql(connectionString);

        return new CrudderDbContext(optionsBuilder.Options);
    }
}

