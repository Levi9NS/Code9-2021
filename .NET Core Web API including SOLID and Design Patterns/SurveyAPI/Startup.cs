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
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SurveyAPI
{
    public class Startup
    {
        private const string CorsPolicy = "CorsPolicy";

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddMvc();
            services.AddTransient<ISurveyService, SurveyService>();
            services.AddScoped<ISurveyRepository, SurveyRepository>();

            services.AddSwaggerGen();

            services.AddCors(options =>
            {
                options.AddPolicy(name: CorsPolicy, builder =>
                {
                    builder.WithOrigins(new string[] { "http://localhost:4200" });
                });
            });
            services.AddDbContext<ApplicationDBContext>(options =>
                options.UseSqlServer("Server=tcp:code9servervukasin.database.windows.net,1433;Initial Catalog=Gode9DatabaseVukasin;Persist Security Info=False;User ID=vukasinvukovic;Password=Plazma123.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

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
