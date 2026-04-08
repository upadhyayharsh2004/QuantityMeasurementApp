using System.Reflection;
using System.Text;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Http;
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
builderWebApplication.Services.AddScoped<IPersonRepository, PersonRepository>();
builderWebApplication.Services.AddScoped<JsonWebTokenService>();
builderWebApplication.Services.AddScoped<IAuthService, AuthenticationService>();


builderWebApplication.Services.AddScoped<EncryptedService>();
builderWebApplication.Services.AddScoped<HashCodeService>();


builderWebApplication.Services.AddHttpContextAccessor();

string jwtSecretKey = builderWebApplication.Configuration["Jwt:SecretKey"] ?? throw new Exception("JWT SecretKey missing");
string jwtIssuer = builderWebApplication.Configuration["Jwt:Issuer"]!;
string jwtAudience = builderWebApplication.Configuration["Jwt:Audience"]!;

builderWebApplication.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtIssuer,
        ValidateAudience = true,
        ValidAudience = jwtAudience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey)),
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };

    options.Events = new JwtBearerEvents
    {
        OnChallenge = async context =>
        {
            context.HandleResponse();
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";
            string body = System.Text.Json.JsonSerializer.Serialize(new
            {
                Timestamp = DateTime.UtcNow.ToString("o"),
                Status = 401,
                Error = "Unauthorized",
                Message = "You must be logged in. Register at POST /api/v1/auth/register or login at POST /api/v1/auth/login",
                Path = context.Request.Path.ToString()
            });
            await context.Response.WriteAsync(body);
        }
    };
});
builderWebApplication.Services.AddAuthorization();
builderWebApplication.Services.AddEndpointsApiExplorer();
builderWebApplication.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Quantity Measurement API",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Paste your JWT token here (without 'Bearer' prefix - Swagger adds it automatically)"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id   = "Bearer"
                }
            },
            new List<string>()
        }
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});
//adding the swagger gen into the code 

builderWebApplication.Services.AddControllers(optionsServices =>
{
    optionsServices.Filters.Add<ApplicationErrorHandler>();
})
.ConfigureApiBehaviorOptions(optionsServices =>
{
    optionsServices.InvalidModelStateResponseFactory = contextServices =>
    {
        var errors = contextServices.ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();

        var responseService = new
        {
            TimestampDTOs = DateTime.UtcNow.ToString("o"),
            StatusDTOs = 400,
            ErrorDTOs = "Validation Request Server Failed",
            MessageDTOs = errors.Any()? string.Join("; ", errors)
                : "Invalid request",
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
if (appServices.Environment.IsDevelopment())
{
    appServices.UseSwagger();
    appServices.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Quantity Measurement API v1");
        options.RoutePrefix = "swagger";
    });
}
//appServices.UseHttpsRedirection();
appServices.UseAuthentication();
appServices.UseAuthorization();

appServices.MapControllers();

appServices.MapHealthChecks("/health");

appServices.Run();
