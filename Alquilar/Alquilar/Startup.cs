using Alquilar.DAL;
using Alquilar.Helpers.Consts;
using Alquilar.Models;
using Alquilar.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Alquilar.Services.Interfaces;
using Alquilar.API.Middlewares;

namespace Alquilar
{
    public class Startup
    {
        private readonly string _allOriginsPolicy = "allOriginsPolicy";

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
            services.AddScoped<NoticiaRepo>();
            services.AddScoped<TipoInmuebleRepo>();
            services.AddScoped<ImagenRepo>();
            services.AddScoped<UsuarioRepo>();
            services.AddScoped<InmuebleRepo>();
            services.AddScoped<HorarioRepo>();
            services.AddScoped<TurnoAsignadoRepo>();
            services.AddScoped<ConfigRepo>();
            #endregion

            #region Services
            services.AddScoped<LocalidadService>();
            services.AddScoped<ProvinciaService>();
            services.AddScoped<RolService>();
            services.AddScoped<NoticiaService>();
            services.AddScoped<TipoInmuebleService>();
            services.AddScoped<ImagenService>();
            services.AddScoped<UsuarioService>();
            services.AddScoped<InmuebleService>();
            services.AddScoped<AuthService>();
            services.AddScoped<TokenService>();
            services.AddScoped<HorarioService>();
            services.AddScoped<TurnoAsignadoService>();
            services.AddSingleton<EmailService>();
            services.AddScoped<ConfigService>();
            services.AddScoped<ITokenService, TokenService>();
            #endregion

            #region Settings
            services.Configure<JwtSettings>(Configuration.GetSection(Settings.JwtSettings));
            services.Configure<EmailSettings>(Configuration.GetSection(Settings.EmailSettings));
            services.Configure<FrontendSettings>(Configuration.GetSection(Settings.FrontendSettings));
            #endregion

            #region JWT
            services
                .AddHttpContextAccessor()
                .AddAuthorization()
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["JwtSettings:Issuer"],
                        ValidAudience = Configuration["JwtSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(Configuration["JwtSettings:Secret"]))
                    };
                });
            #endregion

            services.AddCors(options =>
            {
                options.AddPolicy(_allOriginsPolicy, 
                    builder => builder
                        .AllowAnyOrigin()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .Build());
            });
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(_allOriginsPolicy);

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<TokenMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints
                    .MapControllers()
                    .RequireAuthorization(); // Forcing users to be Authenticated for every endpoint. In order to bypass this, we can use [AllowAnonymous] attribute tag
            });
        }
    }
}
