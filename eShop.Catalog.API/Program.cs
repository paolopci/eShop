using eShop.Catalog.API.Data;
using eShop.Catalog.API.Data.Migrations;
using eShop.Catalog.API.Types;
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
    // Configura le regole globali di paginazione per i campi che usano paging.
    .ModifyPagingOptions(options =>
    {
        // Numero di elementi restituiti di default quando il client non specifica first/last.
        options.DefaultPageSize = 2;
        // Limite massimo di elementi richiedibili in una singola pagina.
        options.MaxPageSize = 5;
        // Disabilita la paginazione backward (before/last).
        // I “boundary di paging” sono i limiti che definiscono quale fetta di risultati
        // vuoi.Nel tuo caso (Relay-style paging GraphQL) sono tipicamente:first e after
        // per andare avanti;last e before per andare indietro.
        options.AllowBackwardPagination = false;
        // Richiede che il client invii esplicitamente i boundary di paging.
        // Con RequirePagingBoundaries = true, il client deve passare almeno
        // un limite (first o last), quindi non può fare query paginata “senza confini”.
        // Questo evita richieste troppo vaghe o potenzialmente pesanti.
        options.RequirePagingBoundaries = true;
    })
    // Abilita la proiezione dei campi GraphQL verso IQueryable/EF.
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
