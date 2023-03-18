using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
using System.Reflection;
using UCVersioning.Utility;

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
        services.AddApiVersioning(opt =>
        {
            opt.DefaultApiVersion = new ApiVersion(0, 1);
            opt.AssumeDefaultVersionWhenUnspecified = true;

            opt.ReportApiVersions = true;
            opt.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader()
            );

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "development")
            {
                //Add controllers that are supposed to only be available in dev or test here:
                //Example: 
                // opt.Conventions.Controller<Controllers.v2.CustomersController>().HasApiVersions(new List<ApiVersion>{ new(2, 0, "pre") });
                //----
                opt.Conventions.Controller<Controllers.v3.OpportunitiesController>().HasApiVersions(new List<ApiVersion> { new(3, 0, "pre") });
            }
            else
            {
                //Together with the above if, the Controllers need to have no api version for production.
                //Example: 
                // opt.Conventions.Controller<Controllers.v2.CustomersController>().HasApiVersions(new List<ApiVersion>());
                //----
                opt.Conventions.Controller<Controllers.v3.OpportunitiesController>().HasApiVersions(new List<ApiVersion>());
            }
        });

        services.AddVersionedApiExplorer(setup =>
        {
            setup.GroupNameFormat = "'v'VVV";
            setup.SubstituteApiVersionInUrl = true;
        });

        services.AddEndpointsApiExplorer();


        services.AddSwaggerGen(c =>
        {
            c.EnableAnnotations(enableAnnotationsForInheritance: true, enableAnnotationsForPolymorphism: true);

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });
        services.ConfigureOptions<ConfigureSwaggerOptions>();

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
            var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                var apiDescriptions = apiVersionDescriptionProvider.ApiVersionDescriptions.OrderBy(x => x.IsDeprecated).ThenBy(x => x.GroupName.ToLower().Contains("pre")).ThenByDescending(x => x.ApiVersion);

                foreach (var description in apiDescriptions)
                {
                    var deprecatedText = description.IsDeprecated ? " (DEPRECATED)" : string.Empty;
                    var isPreview = description.GroupName.Contains("pre");
                    var previewText = isPreview ? " (PREVIEW)" : string.Empty;
                    var text = description.IsDeprecated ? deprecatedText : isPreview ? previewText : string.Empty;

                    c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                        $"{description.GroupName.ToUpperInvariant()}{text}");
                }

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