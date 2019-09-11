using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MBM.DL.Data;
using MBM.DL.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace MBM.WebAPI
{
    /// <summary>
    /// The Start up file
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup" /> class.
        /// </summary>
        /// <param name="env">The hosting environment</param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
            .AddEnvironmentVariables();

            Configuration = builder.Build();
        }
        /// <summary>
        /// Gets the configuration settings
        /// </summary>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// Services to be run for the program 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            Dictionary<string, string> connectionStrings = new Dictionary<string, string>();
            connectionStrings.Add(DataSource.MoneyBMine.ToString() ,Configuration.GetConnectionString("MoneyBMine"));

            services.AddTransient<DataAccessService, DataAccessService>(serviceProvider =>
            {
                return new DataAccessService(connectionStrings);
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MBM.WebAPI", Version = "v1" });
            });

            // Register repositories for dependency injection
            services.AddSingleton<IStockRepository, StockRepository>();
        }


        /// <summary>
        /// Configure the application
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            // Enables Middleware to server generated swagger as Json endpoint.
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/Swagger/v1/swagger.json", "MBM.WebAPI V1");
            });
            app.UseHttpsRedirection();
            app.UseMvc(Config =>
            {
            });
        }
    }
}
