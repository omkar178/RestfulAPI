using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;

namespace RestfulAPI
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ConfigureSwaagerOptions : IConfigureOptions<SwaggerGenOptions>
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        private readonly IApiVersionDescriptionProvider _provider;
        /// <summary>
        /// Initalized ApiVersion Description provider.
        /// </summary>
        /// <param name="provider"></param>
        public ConfigureSwaagerOptions(IApiVersionDescriptionProvider provider) => _provider = provider;   
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var desc in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(desc.GroupName, new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = $"Restful Api {desc.ApiVersion}",
                    Version = desc.ApiVersion.ToString()
                    //Description = "RESTFUL API NP",
                    //Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                    //{
                    //    Name = "Omkar Navik",
                    //    Email = "Navik46@gmail.com",
                    //    Url = new Uri("https://github.com/omkar178/RestfulAPI/tree/master/RestfulAPI")
                    //},
                    //License = new Microsoft.OpenApi.Models.OpenApiLicense()
                    //{
                    //    Name = "GIT License",
                    //    Url = new Uri("https://github.com/omkar178/RestfulAPI")
                    //}
                });
            }

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description =
               "JWT Authorization header using the Bearer scheme. \r\n\r\n " +
               "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
               "Example: \"Bearer 12345abcdef\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
        }
    }
}
