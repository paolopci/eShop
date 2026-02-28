var builder = WebApplication.CreateBuilder(args);


builder.Services.AddGraphQLServer();

var app = builder.Build();



app.MapGraphQL();
app.RunWithGraphQLCommands(args);



app.Run();

