using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Models;
using ERPAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;

[assembly:ApiConventionType(typeof(DefaultApiConventions))]


namespace ERPAPI
{
    public class Startup
    {
        private readonly ILogger _logger;

        public Startup(IConfiguration configuration
            , ILogger<Startup> logger
            )
        {
            Configuration = configuration;
            _logger = logger;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<HashService>();
            services.AddDataProtection();

            services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")
                    ));

            services.AddIdentity<ApplicationUser,  ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.AddAutoMapper();

            services.AddLogging();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(Configuration["jwt:key"])),
                ClockSkew = TimeSpan.Zero
            });

            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info {
                    Title = "BI ERP API",
                    Version = "v1",
                    Description ="Esta es una descripcion del Web API",
                    TermsOfService = "https://www.bi-dss.com",
                    License = new Swashbuckle.AspNetCore.Swagger.License
                    {
                        Name ="MIT",
                        Url= "https://www.bi-dss.com"
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                config.IncludeXmlComments(xmlPath);
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        /*private string GetConnection(HttpRequest _request)
        {
            string db2ConnectionString = "";
            var databaseQuerystringParameter = "";
            databaseQuerystringParameter = _request.Query["database"].ToString();
             db2ConnectionString = Configuration.GetConnectionString("DefaultConnection");

            _logger.LogInformation($"Parametro de base de datos{databaseQuerystringParameter}");
            if (databaseQuerystringParameter != "" && databaseQuerystringParameter != null)
            {
                _logger.LogInformation($"Distinto de nulo");
                db2ConnectionString = string.Format(db2ConnectionString, databaseQuerystringParameter);
            }
            else
            {
                db2ConnectionString = Configuration.GetConnectionString("DefaultConnection");
            }
            return db2ConnectionString;

        }*/

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "BI ERP V1");

            });

            app.UseCors(b => b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
