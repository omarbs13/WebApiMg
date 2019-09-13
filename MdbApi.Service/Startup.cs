using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using MdbApi.Application.Interface;
using MdbApi.Application.Main;
using MdbApi.Application.Models;
using MdbApi.Data;
using MdbApi.Data.Interface;
using MdbApi.Data.Repository;
using MdbApi.Domain.Entities;
using MdbApi.Service.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace MdbApi.Service
{
    public class Startup
    {
        readonly string myPolicy = "policyApi";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
           {
               mc.AddProfile(new MappingsProfile());
           });
            // services.AddAutoMapper(x => x.AddProfile(new MappingsProfile()));
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddCors(options => options.AddPolicy(myPolicy, builder => builder.WithOrigins(Configuration["Config:OriginCors"])
                                                                                        .AllowAnyHeader()
                                                                                        .AllowAnyMethod()));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(options => { options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver(); }); ;

            var appSettingsSection = Configuration.GetSection("Config");

            services.AddOptions();
            services.Configure<AppSettings>(appSettingsSection);
            var con = Configuration.GetConnectionString("DefaultConnectionString");
            services.Configure<Settings>(options =>
            {
                options.ConnectionString = Configuration.GetConnectionString("DefaultConnectionString");
                options.Database = Configuration.GetConnectionString("Database");
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserApplication, UserApplication>();

            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));


            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Ceron Technology Services API",
                    Description = "ASP.NET Core Web API",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "Omar Ceron Ochoa",
                        Email = "omarbs13@gmail.com",
                        Url = ""
                    },
                    License = new License
                    {
                        Name = "Use under ceron",
                        Url = ""
                    }
                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.AddSecurityDefinition("Authorization", new ApiKeyScheme
                {
                    Description = "Authorization by API key.",
                    In = "header",
                    Type = "apiKey",
                    Name = "Authorization"
                });

                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Authorization", new string[0] }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseCors(myPolicy);
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }

}
