using Microsoft.Net.Http.Headers;

namespace Library.Api.DI;

public static class DISwaggerApplication
{
    public static IServiceCollection AddSwaggerApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(options =>
        {
            options.EnableAnnotations();
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Travel - Library HTTP API",
                Version = "v1",
                Description = "The Library Service HTTP API"
            });

            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows()
                {
                    //Implicit = new OpenApiOAuthFlow()
                    //{
                    //    AuthorizationUrl = new Uri($"{configuration.GetValue<string>("IdentityUrl")}/connect/authorize"),
                    //    TokenUrl = new Uri($"{configuration.GetValue<string>("IdentityUrl")}/connect/token"),
                    //    Scopes = new Dictionary<string, string>()
                    //    {
                    //        { "library", "Library API" }
                    //    }
                    //}

                    ClientCredentials = new OpenApiOAuthFlow
                    {
                        Scopes = new Dictionary<string, string> {
                            {"library", "Library Api" }
                        },
                        TokenUrl = new Uri($"{configuration.GetValue<string>("IdentityUrl")}/connect/token")
                    }
                },
                In = ParameterLocation.Header,
                Name = HeaderNames.Authorization

            }); ;
            options.AddSecurityRequirement(
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                                { Type = ReferenceType.SecurityScheme, Id = "oauth" },
                        },
                        new[] { "library" }
                    }
                });

            options.OperationFilter<AuthorizeCheckOperationFilter>();
        });

        return services;

    }
}

