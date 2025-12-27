using AnimeList.API.Extensions;
using AnimeList.API.HostedServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApi();
builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddHostedService<DatabaseStartupService>();

var app = builder.Build();

app.MapControllers();

app.Run();
