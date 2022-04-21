using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestfulAPI.Data;
using AutoMapper;
using System;
using RestfulAPI.Mappings;
using RestfulAPI.Repository;
using RestfulAPI.Repository.IRepository;
using System.Reflection;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace RestfulAPI
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Startup

    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<INationalParkRepository,NationalParkRepository>();
            services.AddScoped<ITrailsRepository, TrailsRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddAutoMapper(typeof(NationalParkMappings));
            services.AddApiVersioning(options => {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            });
            
            services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaagerOptions>();          
            services.AddSwaggerGen();

            var appsettingSection = Configuration.GetSection("AppSetting");
            services.Configure<AppSetting>(appsettingSection);

            var appSetting = appsettingSection.Get<AppSetting>();
            var key = Encoding.ASCII.GetBytes(appSetting.secretKey);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x => {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });






            //services.AddSwaggerGen(options => {
            //    options.SwaggerDoc("RestfulOpenApiSpecificationNP", new Microsoft.OpenApi.Models.OpenApiInfo()
            //    {
            //        Title = "RESTFUL API NP",
            //        Version = "1.0",
            //        Description = "RESTFUL API NP",
            //        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
            //        {
            //            Name = "Omkar Navik",
            //            Email = "Navik46@gmail.com",
            //            Url = new Uri("https://github.com/omkar178/RestfulAPI/tree/master/RestfulAPI")
            //        },
            //        License = new Microsoft.OpenApi.Models.OpenApiLicense()
            //        {
            //            Name = "GIT License",
            //            Url = new Uri("https://github.com/omkar178/RestfulAPI"),

            //        }
            //    });

            //options.SwaggerDoc("RestfulOpenApiSpecificationTrail", new Microsoft.OpenApi.Models.OpenApiInfo()
            //{
            //    Title = "RESTFUL API Trail",
            //    Version = "1.0",
            //    Description = "RESTFUL API Trail",
            //    Contact = new Microsoft.OpenApi.Models.OpenApiContact()
            //    {
            //        Name = "Omkar Navik",
            //        Email = "Navik46@gmail.com",
            //        Url = new Uri("https://github.com/omkar178/RestfulAPI/tree/master/RestfulAPI")
            //    },
            //    License = new Microsoft.OpenApi.Models.OpenApiLicense()
            //    {
            //        Name = "GIT License",
            //        Url = new Uri("https://github.com/omkar178/RestfulAPI"),

            //    }
            //});

            //    var xmlCommentFilePath = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //    var fullPath = Path.Combine(AppContext.BaseDirectory,xmlCommentFilePath);
            //    options.IncludeXmlComments(fullPath);
            //});
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(options => {
                foreach (var desc in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json",desc.GroupName.ToUpperInvariant());
                }
                options.RoutePrefix = "";
            });

            //app.UseSwaggerUI(options => {
            //    options.SwaggerEndpoint("/swagger/RestfulOpenApiSpecification/swagger.json", "RestfulAPI");
            //   // options.SwaggerEndpoint("/swagger/RestfulOpenApiSpecificationTrail/swagger.json", "RestfulAPI_Trail");
            //    options.RoutePrefix = "";
               
            //});
            app.UseRouting();
            app.UseCors(x => x.
            AllowAnyHeader().
            AllowAnyOrigin().
            AllowAnyOrigin());
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
