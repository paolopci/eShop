using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace eShop.Catalog.API.Data;

internal sealed class CatalogContextFactory : IDesignTimeDbContextFactory<CatalogContext>
{
    public CatalogContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddUserSecrets<Program>(optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString("CatalogDB")
            ?? configuration["ConnectionStrings:CatalogDB"]
            ?? throw new InvalidOperationException("Connection string 'CatalogDB' non trovata.");

        var optionsBuilder = new DbContextOptionsBuilder<CatalogContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new CatalogContext(optionsBuilder.Options);
    }
}
