using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Crossover.LBS.API.Business;
using Crossover.LBS.API.Business.Domain;
using Crossover.LBS.API.Business.MapperProfiles;
using Crossover.LBS.API.Contracts.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.Swagger.Model;

namespace Crossover.LBS.API
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();

            Mapper.Initialize(cfg => {
                cfg.AddProfile<LbsProfile>();
            });

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyHeadersAndMethods",
                    builder =>
                    {
                        builder.AllowAnyHeader().AllowAnyMethod();
                    });
            });

            services.AddDbContext<LbsContext>(options => options.UseSqlServer(Configuration.GetValue<string>("Data:DefaultConnection:ConnectionString")));

            services.AddScoped<ICredentialManager, CredentialManager>();
            services.AddScoped<IMachineManager, MachineManager>();
            services.AddScoped<ILogManager, LogManager>();

            services.AddMvc();

            // Swagger options
            services.AddSwaggerGen();
            services.ConfigureSwaggerGen(options =>
            {
                string xmlCommentsPath =
                    $@"{AppDomain.CurrentDomain.BaseDirectory}{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "LBS API",
                    Description = "LAN Backup System API",
                    TermsOfService = "None"
                });
                options.IncludeXmlComments(xmlCommentsPath);
                options.DescribeAllEnumsAsStrings();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseCors("AllowAnyHeadersAndMethods");

            if (!env.IsProduction())
            {
                app.UseSwagger();
                // if it's in QA add virtual directory name
                app.UseSwaggerUi(swaggerUrl: (env.IsDevelopment() ? string.Empty : "/" + new DirectoryInfo(env.ContentRootPath).Name) + "/swagger/v1/swagger.json");
            }


            app.UseMvc();

        }
    }
}
