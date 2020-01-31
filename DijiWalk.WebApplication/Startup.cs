using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DijiWalk.EntitiesContext;
using DijiWalk.Repositories;
using DijiWalk.Repositories.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using VueCliMiddleware;

namespace DijiWalk.WebApplication
{
    public static class CustomSetting{
        public static void AddCustomSettings(this JsonSerializerSettings settings)
        {
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        }
    }

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
            services.AddControllers().AddNewtonsoftJson(option => option.SerializerSettings.AddCustomSettings());
            services.AddSpaStaticFiles(configuration => configuration.RootPath = "DijiWalk");
            services.AddScoped<IAdministratorRepository, AdministratorRepository>();
            services.AddScoped<IAnswerRepository,  AnswerRepository>();
            services.AddScoped<IGameRepository,  GameRepository>();
            services.AddScoped<IMessageRepository,  MessageRepository>();
            services.AddScoped<IMissionRepository,  MissionRepository>();
            services.AddScoped<IOrganizerRepository,  OrganizerRepository>();
            services.AddScoped<IPlayerRepository,  PlayerRepository>();
            services.AddScoped<IPlayRepository,  PlayRepository>();
            services.AddScoped<IRouteRepository,  RouteRepository>();
            services.AddScoped<IRouteStepRepository,  RouteStepRepository>();
            services.AddScoped<IRouteTagRepository,  RouteTagRepository>();
            services.AddScoped<IStepRepository,  StepRepository>();
            services.AddScoped<ITagRepository,  TagRepository>();
            services.AddScoped<ITeamAnswerRepository,  TeamAnswerRepository>();
            services.AddScoped<ITeamPlayerRepository,  TeamPlayerRepository>();
            services.AddScoped<ITeamRepository,  TeamRepository>();
            services.AddScoped<ITeamRouteRepository,  TeamRouteRepository>();
            services.AddScoped<ITransportRepository,  TransportRepository>();
            services.AddScoped<ITrialRepository,  TrialRepository>();
            services.AddScoped<ITypeRepository,  TypeRepository>();
            services.AddDbContext<SmartCityContext>(optionsBuilder => optionsBuilder.UseSqlServer("Server=NATHAN-PC;Database=SmartCity;Trusted_Connection=True;MultipleActiveResultSets=true"));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSpaStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                if (env.IsDevelopment())
                {
                    spa.Options.SourcePath = "DijiWalk";
                    spa.UseVueCli(npmScript: "serve");
                }
                else
                    spa.Options.SourcePath = "dist";
            });
        }
    }
}
