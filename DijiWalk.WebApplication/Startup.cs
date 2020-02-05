using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DijiWalk.Common.Contracts;
using DijiWalk.Common.Encryption;
using DijiWalk.Common.Response;
using DijiWalk.EntitiesContext;
using DijiWalk.Repositories;
using DijiWalk.Repositories.Contracts;
using DijiWalk.WebApplication.Models;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using VueCliMiddleware;

namespace DijiWalk.WebApplication
{
    public static class CustomSetting{

        /// <summary>
        /// Disable circular dependency
        /// </summary>
        /// <param name="settings"></param>
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
            services.AddCors();
            services.AddControllers().AddNewtonsoftJson(option => option.SerializerSettings.AddCustomSettings());
            services.AddSpaStaticFiles(configuration => configuration.RootPath = "DijiWalk");
            services.AddAntiforgery(x => x.HeaderName = "X-XSRF-TOKEN");

            #region Repositories
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
            #endregion

            #region Business Layer
            services.AddScoped<IAuthentificationRepository, AuthentificationRepository>();
            #endregion

            #region Common
                services.AddScoped<ICryption, Cryption>();
                services.AddScoped<IApiResponse, ApiResponse>();
            #endregion

            #region JWT Token
            var jwtSection = Configuration.GetSection("JWTSettings");
            services.Configure<JWTSettings>(jwtSection);

            //to validate the token which has been sent by clients
            var appSettings = jwtSection.Get<JWTSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.SecretKey);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme; 
            })
            .AddJwtBearer(x =>
            {
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
            #endregion

            services.AddDbContext<SmartCityContext>(optionsBuilder => optionsBuilder.UseSqlServer(Configuration.GetConnectionString("LocalConnection")));
            services.AddSingleton(Configuration);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IAntiforgery antiforgery)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.Use(next => context =>
            {
                if ( string.Equals(context.Request.Path.Value, "/"))
                {
                    var token = antiforgery.GetAndStoreTokens(context).RequestToken;
                    context.Response.Cookies.Append("XSRF-TOKEN", token, new CookieOptions { HttpOnly = false, Secure = true });
                }

                return next(context);
            });

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
