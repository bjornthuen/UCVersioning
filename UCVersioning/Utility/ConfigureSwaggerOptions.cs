using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace UCVersioning.Utility;

/// <summary>
/// Swagger Options
/// </summary>
public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider provider;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="provider"></param>
    public ConfigureSwaggerOptions(
        IApiVersionDescriptionProvider provider)
    {
        this.provider = provider;
    }

    /// <summary>
    /// Configure each API discovered for Swagger Documentation
    /// </summary>
    /// <param name="options"></param>
    public void Configure(SwaggerGenOptions options)
    {
        // add swagger document for every API version discovered
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(
                description.GroupName,
                CreateVersionInfo(description));
        }
    }

    /// <summary>
    /// Configure Swagger Options. Inherited from the Interface
    /// </summary>
    /// <param name="name"></param>
    /// <param name="options"></param>
    public void Configure(string name, SwaggerGenOptions options)
    {
        Configure(options);
    }

    /// <summary>
    /// Create information about the version of the API
    /// </summary>
    /// <param name="description"></param>
    /// <returns>Information about the API</returns>
    private OpenApiInfo CreateVersionInfo(
        ApiVersionDescription description)
    {
        var info = new OpenApiInfo()
        {
            Title = "UC Versioning API",
            Version = description.ApiVersion.ToString(),
            Description = "UC Versioning API.",
            Contact = new OpenApiContact
            {
                Name = "Git Repository",
                Url = new Uri("https://github.com/bjornthuen/UCVersioning")
            }
        };

        if (description.IsDeprecated)
        {
            info.Description = "This API version has been deprecated. Please use one of the new APIs available from the explorer.";
        }

        if (description.GroupName.Contains("pre"))
        {
            info.Title = $"PREVIEW: UC Versioning API";
            info.Description = $"PREVIEW of upcoming changes and features of the API.";
        }

        return info;
    }
}