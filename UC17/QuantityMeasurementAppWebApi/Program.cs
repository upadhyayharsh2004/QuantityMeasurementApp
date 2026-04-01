using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using QuantityMeasurementApp.Services;
using QuantityMeasurementAppRepositories.Context;
using QuantityMeasurementAppRepositories.Interfaces;
using QuantityMeasurementAppRepositories.Repositories;
using QuantityMeasurementAppServices.Interfaces;

using QuantityMeasurementAppServices.Services;
using QuantityMeasurementWebApi.Middleware;

var builderWebApplication = WebApplication.CreateBuilder(args);

builderWebApplication.Services.AddDbContext<DatabaseAppContext>(options => options.UseSqlServer(builderWebApplication.Configuration.GetConnectionString("DefaultConnection")));

builderWebApplication.Services.AddScoped<IQuantityLogRepository, QuantityRepository>();
builderWebApplication.Services.AddScoped<IQuantityServiceImplsConvert, QuantityImplService>();
builderWebApplication.Services.AddScoped<IQuantityServiceImplWeb, QuantityWebServiceImpl>();

builderWebApplication.Services.AddEndpointsApiExplorer();
builderWebApplication.Services.AddSwaggerGen(options =>
{
    var xmlFile = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "QuantityMeasurementAppWebAPI.xml");

    if (File.Exists(xmlFile))
        options.IncludeXmlComments(xmlFile);
});

builderWebApplication.Services.AddControllers(optionsServices =>
{
    optionsServices.Filters.Add<ApplicationErrorHandler>();
})
.ConfigureApiBehaviorOptions(optionsServices =>
{
    optionsServices.InvalidModelStateResponseFactory = contextServices =>
    {
        var errors = contextServices.ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);

        var responseService = new
        {
            TimestampDTOs = DateTime.UtcNow.ToString("o"),
            StatusDTOs = 400,
            ErrorDTOs = "Validation Request Server Failed",
            MessageDTOs = string.Join("; ", errors),
            PathDTOs = contextServices.HttpContext.Request.Path.ToString()
        };

        return new Microsoft.AspNetCore.Mvc.BadRequestObjectResult(responseService);
    };
});

builderWebApplication.Services.AddHealthChecks();

var appServices = builderWebApplication.Build();


using (var scope = appServices.Services.CreateScope())
{
    var databaseMigrate = scope.ServiceProvider.GetRequiredService<DatabaseAppContext>();
    databaseMigrate.Database.Migrate();
}
appServices.UseSwagger();

appServices.UseSwaggerUI();

appServices.MapControllers();

appServices.MapHealthChecks("/health");

appServices.Run();
