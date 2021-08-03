using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using GamesCatalogAPI.Services;
using GamesCatalogAPI.Repositories;
using System;
using System.Reflection;
using System.IO;
using GamesCatalogAPI.Middleware;

namespace GamesCatalogAPI {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration {
            get;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddControllers();
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GamesCatalogAPI", Version = "v1" });

                //folder application
                var basePah = AppDomain.CurrentDomain.BaseDirectory;
                //Get name application
                var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
                //Indicates this file to be used by Swagger 
                c.IncludeXmlComments(Path.Combine(basePah, fileName));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GamesCatalogAPI v1"));
            }

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
