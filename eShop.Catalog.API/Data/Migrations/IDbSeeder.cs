using Microsoft.EntityFrameworkCore;

namespace eShop.Catalog.API.Data.Migrations;

public interface IDbSeeder<in TContext> where TContext : DbContext
{
    Task SeedAsync(TContext context);
}
