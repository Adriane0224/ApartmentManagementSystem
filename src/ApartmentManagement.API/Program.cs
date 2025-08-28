using ApartmentManagement.Contracts.Services;
using Directory.Application;
using Directory.Infrastructure;
using Directory.Infrastructure.MappingProfiles;
using Leasing.Application;
using Leasing.Infrastructure;
using Leasing.Infrastructure.MappingProfiles;
using Property.Application;
using Property.Infrastructure;
using Property.Infrastructure.MappingProfiles;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Leasing.Application.AssemblyReference).Assembly);
    //cfg.RegisterServicesFromAssembly(typeof(Property.Application.AssemblyReference).Assembly);
});

//Leasing
builder.Services.AddLeasingApplication();
builder.Services.AddLeasingInfrastructure(builder.Configuration);

//Property
builder.Services.AddApartmentApplication();
builder.Services.AddApartmentInfrastructure(builder.Configuration);

builder.Services.AddAutoMapper(cfg =>
{
   cfg.AddMaps(typeof(ApartmentMappingProfile).Assembly);
    cfg.AddMaps(typeof(LeaseMappingProfile).Assembly);
    cfg.AddMaps(typeof(TenantMappingProfile).Assembly);
});

//Directory
builder.Services.AddDirectoryApplication();
builder.Services.AddDirectoryInfrastructure(builder.Configuration);


builder.Services.AddScoped<IEventBus, EventBus>();
builder.Services.AddScoped<IDomainEventPublisher, DomainEventPublisher>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => Results.Redirect("/scalar/v1"));

app.Run();
