using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SurveyAPI.Interfaces;
using SurveyAPI.Models;
using SurveyAPI.Repositories;
using SurveyAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyAPI
{
    public class Startup
    {
        private const string CorsPolicy = "CorsPolicy";

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddTransient<ISurveyService, SurveyService>();
            services.AddScoped<ISurveyRepository, SurveyRepository>();

            services.AddCors(options =>
            {
                options.AddPolicy(name: CorsPolicy, builder =>
                {
                    builder.WithOrigins(new string[] { "http://localhost:4200" });
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(CorsPolicy);

            app.UseRouting();

            // Ensure to map the controllers accordingly
            app.UseEndpoints(endpoints =>
            {
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Survey}/{action=Index}");
                });
            });
        }
    }
}
