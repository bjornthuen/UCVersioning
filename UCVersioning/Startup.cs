using Microsoft.OpenApi.Models;
using System.Reflection;

namespace UCVersioning;

/// <summary>
/// Startup
/// </summary>
public class Startup
{
    private IConfiguration Configuration { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configuration"></param>
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    /// <summary>
    /// Configuration service
    /// </summary>
    /// <param name="services"></param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("Unversioned", new OpenApiInfo()
            {
                Title = "UC Versioning API",
                Description = "UC Versioning API.",
                Contact = new OpenApiContact
                {
                    Name = "Git Repository",
                    Url = new Uri("https://github.com/bjornthuen/UCVersioning")
                },
            });

            c.EnableAnnotations(enableAnnotationsForInheritance: true, enableAnnotationsForPolymorphism: true);

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });

        services.AddControllers();
    }

    /// <summary>
    /// Configure
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/Unversioned/swagger.json", "Unversioned");
                c.RoutePrefix = string.Empty;

                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "development")
                {
                    c.DisplayRequestDuration();
                    c.InjectStylesheet("/assets/css/uc-style-dev.css");
                }
                else
                {
                    c.InjectStylesheet("/assets/css/uc-style-prod.css");
                }
            });
        }

        app.UseStaticFiles();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}