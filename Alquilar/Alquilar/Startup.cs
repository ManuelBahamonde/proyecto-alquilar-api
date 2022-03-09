using Alquilar.DAL;
using Alquilar.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alquilar
{
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
            #region Database
            var connString = Configuration.GetConnectionString("development");
            services.AddDbContext<DB>(options => options.UseSqlServer(connString));
            #endregion

            #region Repositories
            services.AddScoped<LocalidadRepo>();
            services.AddScoped<ProvinciaRepo>();
            services.AddScoped<RolRepo>();
            services.AddScoped<TipoInmuebleRepo>();
            services.AddScoped<ImagenRepo>();
            services.AddScoped<UsuarioRepo>();
            services.AddScoped<InmuebleRepo>();
            #endregion

            #region Services
            services.AddScoped<LocalidadService>();
            services.AddScoped<ProvinciaService>();
            services.AddScoped<RolService>();
            services.AddScoped<TipoInmuebleService>();
            services.AddScoped<ImagenService>();
            services.AddScoped<UsuarioService>();
            services.AddScoped<InmuebleService>();
            #endregion

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Alquilar", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Alquilar v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
