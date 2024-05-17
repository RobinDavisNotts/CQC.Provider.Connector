using CQC.Provider.Connector.Core.Entities.Options;
using CQC.Provider.Connector.Core.Repositories;
using CQC.Provider.Connector.Core.Services;
using CQC.Provider.Connector.Data.Contexts;
using CQC.Provider.Connector.Data.Services;
using CQC.Provider.Connector.Features.Providers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mongoDbSettings = builder.Configuration.GetSection(nameof(MongoDbConfig)).Get<MongoDbConfig>();
builder.Services.Configure<CQCApiOptions>(builder.Configuration.GetSection("CQCApi"));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IProviderRepository, ProviderRepository>();
builder.Services.AddHttpClient<IProviderHttpService, ProviderHttpService>();
builder.Services.AddScoped<IProvidersHandler, ProvidersHandler>();

builder.Services.AddDbContext<ProviderContext>(options =>
{
    options.UseMongoDB(mongoDbSettings.ConnectionString, mongoDbSettings.Name);
});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.AddProvidersEndpoints();


app.Run();