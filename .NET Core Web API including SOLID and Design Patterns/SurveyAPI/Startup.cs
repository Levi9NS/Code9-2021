using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SurveyAPI.DBModels;
using SurveyAPI.Interfaces;
using SurveyAPI.Models;
using SurveyAPI.Repositories;
using SurveyAPI.Services;


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

            services.AddScoped<IGeneraInformationsRepository, GeneraInformationsRepository>();
            services.AddScoped<IAnswersRepository, AnswersRepository>();
            services.AddScoped<IOfferedAnswersRepository, OfferedAnswersRepository>();
            services.AddScoped<IParticipantsRepository, ParticipantsRepository>();
            services.AddScoped<IQuestionsRepository, QuestionsRepository>();
            services.AddScoped<ISurveyQuestionRelationRepository, SurveyQuestionRelationRepository>();
            services.AddScoped<IQuestionOfferedAnswerRelationRepository, QuestionOfferedAnswerRelationRepository>();

            services.AddSwaggerGen();

            services.AddCors(options =>
            {
                options.AddPolicy(name: CorsPolicy, builder =>
                {
                    builder.WithOrigins(new string[] { "http://localhost:4200" });
                });
            });

            services.AddDbContext<SurveyConetxt>(options =>
                options.UseSqlServer(""));
        
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
