using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;
using System.IO;
using System.Reflection;
using Swashbuckle.AspNetCore.Swagger;
using MoneyChanger.Martin.Sun.Infra;

namespace MoneyChanger.Martin.Sun
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
     
        public Startup(IConfiguration configuration)
        {
            Configuration = new ConfigurationBuilder()
              .AddConfiguration(configuration)          
                .Build();
           
        }
    
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IMoneyChangerService, MoneyChangrerService>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddApiVersioning(o => o.ReportApiVersions = true);
            services.AddMvcCore().AddVersionedApiExplorer(
             options =>
             {
                 options.GroupNameFormat = "'v'V";
                      // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                      // can also be used to control the format of the API version in route templates
                      options.SubstituteApiVersionInUrl = true;
             });
            services.AddSwaggerGen(
               options =>
               {
                    // resolve the IApiVersionDescriptionProvider service
                    // note: that we have to build a temporary service provider here because one has not been created yet
                    var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
                    // add a swagger document for each discovered API version
                    // note: you might choose to skip or document deprecated API versions differently
                    foreach (var description in provider.ApiVersionDescriptions)
                   {
                       options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
                   }
                    // add a custom operation filter which sets default values
                    options.OperationFilter<SwaggerDefaultValues>();
                    // https://stackoverflow.com/questions/46071513/swagger-error-conflicting-schemaids-duplicate-schemaids-detected-for-types-a-a
                    options.CustomSchemaIds(x => x.FullName);
                    // integrate xml comments
                    options.IncludeXmlComments(XmlCommentsFilePath);
               });

        }
        // Configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        { 
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(
              options =>
              {
                    // build a swagger endpoint for each discovered API version
                    foreach (var description in provider.ApiVersionDescriptions)
                  {
                      options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                  }
              });

        }
        private static string XmlCommentsFilePath
        {
            get
            {
                var basePath = AppContext.BaseDirectory;
                var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
                return Path.Combine(basePath, fileName);
            }
        }
        static Info CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new Info()
            {
                Title = $"Currency Excahnge API {description.ApiVersion}",
                Version = description.ApiVersion.ToString(),
                //Description = "A sample application with Swagger, Swashbuckle, and API versioning.",
                Contact = new Contact() { Name = "Martin", Email = "meowdin@yahoo.com.sg" }
            };
            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }
            return info;
        }
    }
}
