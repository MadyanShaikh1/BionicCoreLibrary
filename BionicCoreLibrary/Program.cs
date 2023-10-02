using BionicCoreLibrary.Authentication;
using BionicCoreLibrary.Infrastructure;
using BionicCoreLibrary.Infrastructure.DependancyExtensions;
using BionicCoreLibrary.Infrastructure.DependancyInjections.SharedDependancy;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Services.LoadConfiguration(builder.Configuration);
//var serviceDescriptor = new ServiceCollection();


// Add services to the 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Authentication(configuration);
builder.Services.InitializeDapper(configuration);
builder.Services.AddInfraStructure();
builder.Services.SharedDependancy();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthentication();
app.UseAuthorization();
//app.UseAuthenticationMiddleware();

app.MapControllers();

app.Run();
