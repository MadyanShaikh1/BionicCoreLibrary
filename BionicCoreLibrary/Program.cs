using BionicCoreLibrary.Authentication;
using BionicCoreLibrary.Common.Constant;
using BionicCoreLibrary.Core.Configuration;
using BionicCoreLibrary.DependancyInjections.DependancyExtensions;
using BionicCoreLibrary.Infrastructure;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Services.LoadConfiguration(builder.Configuration);
var serviceDescriptor = new ServiceCollection();


// Add services to the 
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();
builder.Services.InitializeDapper(configuration);
builder.Services.AddInfraStructure();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Authentication(serviceDescriptor, configuration);
app.UseAuthenticationMiddleware();
//app.UseAuthorization();

app.MapControllers();

app.Run();
