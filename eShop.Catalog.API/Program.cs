using eShop.Catalog.API.Data;
using eShop.Catalog.API.Data.Migrations;
using eShop.Catalog.API.Types;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("CatalogDB")
    ?? throw new InvalidOperationException("Connection string 'CatalogDB' non configurata.");

builder.Services.AddDbContext<CatalogContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddScoped<CatalogContextSeed>();
builder.Services.AddScoped<Query>();
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddProjections();
// .AddMutationType<Mutation>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CatalogContext>();
    await context.Database.MigrateAsync();

    if (app.Environment.IsDevelopment())
    {
        var seeder = scope.ServiceProvider.GetRequiredService<CatalogContextSeed>();
        await seeder.SeedAsync(context);
    }
}

app.MapGraphQL();
app.RunWithGraphQLCommands(args);
app.Run();

