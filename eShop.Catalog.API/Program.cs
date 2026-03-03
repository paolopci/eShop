using eShop.Catalog.API.Data;
using eShop.Catalog.API.Data.Migrations;
using eShop.Catalog.API.Extensions;
using eShop.Catalog.API.Types;
using HotChocolate.Data.Filters;
using HotChocolate.Types.Pagination;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("CatalogDB")
    ?? throw new InvalidOperationException("Connection string 'CatalogDB' non configurata.");

builder.Services.AddDbContext<CatalogContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddScoped<CatalogContextSeed>();
// Registra il tipo Query nel container DI con ciclo di vita scoped.
builder.Services.AddScoped<Query>();
builder.Services
    // Inizializza la pipeline GraphQL di HotChocolate.
    .AddGraphQLServer()
    // Imposta il root Query type dello schema GraphQL.
    .AddQueryType<Query>()
    // rende disponibile la definizione di filtro personalizzata che hai scritto (campi e operazioni consentite);
    .AddType<ProductFilterInputType>()

    .AddGraphQLConventions();



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