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


            //services.AddDbContext<ApplicationDbContext>(options =>
            //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")          
            //), ServiceLifetime.Transient);

            services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")
                    ));



            //_logger.LogInformation($"Antes de agregar el contexto");
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
            //{
            //    var httpContext = serviceProvider.GetService<IHttpContextAccessor>().HttpContext;
            //    var httpRequest = httpContext.Request;
            //    var connection = GetConnection(httpRequest);
            //    options.UseSqlServer(connection);
            //});
            //services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
            //{
            //    var httpContext = serviceProvider.GetService<IHttpContextAccessor>().HttpContext;
            //    _logger.LogInformation(httpContext.ToString());
            //    var httpRequest = httpContext.Request;
            //    var databaseQuerystringParameter = "";              
            //         databaseQuerystringParameter = httpRequest.Query["database"].ToString();
            //    var db2ConnectionString = Configuration.GetConnectionString("DefaultConnection");

            //    _logger.LogInformation($"Parametro de base de datos{databaseQuerystringParameter}");
            //    if (databaseQuerystringParameter != "" && databaseQuerystringParameter != null)
            //    {
            //        _logger.LogInformation($"Distinto de nulo");
            //        // We have a 'database' param, stick it in.
            //        db2ConnectionString = string.Format(db2ConnectionString, databaseQuerystringParameter);
            //    }
            //    else
            //    {
            //        // We havent been given a 'database' param, use the default.
            //         db2ConnectionString = Configuration.GetConnectionString("DefaultConnection");
            //        //db2ConnectionString = string.Format(db2ConnectionString, db2DefaultDatabaseValue);
            //    }

            //    _logger.LogInformation($"Connection string usado:{db2ConnectionString}");

            //    options.UseSqlServer(db2ConnectionString);
            //    //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))

            //});



            //services.AddIdentity<ApplicationUser, IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddDefaultTokenProviders();

            services.AddIdentity<ApplicationUser,  ApplicationRole>()
           .AddEntityFrameworkStores<ApplicationDbContext>()
          // .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>()
           // .AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>()
          //.AddRoleManager<ApplicationRole>()
          // .AddSignInManager<SignInManager<ApplicationUser>>()          
          .AddDefaultTokenProviders();

            

            services.AddAutoMapper(options =>
          {
              //options.CreateMap<AutorCreacionDTO, Autor>();
          });

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
                    },
                    Contact = new Swashbuckle.AspNetCore.Swagger.Contact()
                    {
                        Name = "Freddy Chinchilla",
                        Email="freddy.chinchilla@bi-dss.com",
                        
                        
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                config.IncludeXmlComments(xmlPath);
                // _logger.LogInformation("Agrego los comentarios de los endpoints a swagger! ");
            });

            //  _logger.LogInformation("Arranco! ");
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        private string GetConnection(HttpRequest _request)
        {
            string db2ConnectionString = "";

            var databaseQuerystringParameter = "";
            databaseQuerystringParameter = _request.Query["database"].ToString();
             db2ConnectionString = Configuration.GetConnectionString("DefaultConnection");

            _logger.LogInformation($"Parametro de base de datos{databaseQuerystringParameter}");
            if (databaseQuerystringParameter != "" && databaseQuerystringParameter != null)
            {
                _logger.LogInformation($"Distinto de nulo");
                // We have a 'database' param, stick it in.
                db2ConnectionString = string.Format(db2ConnectionString, databaseQuerystringParameter);
            }
            else
            {
                // We havent been given a 'database' param, use the default.
                db2ConnectionString = Configuration.GetConnectionString("DefaultConnection");
                //db2ConnectionString = string.Format(db2ConnectionString, db2DefaultDatabaseValue);
            }

            return db2ConnectionString;

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "BI ERP V1");

            });

            app.UseCors(b => b.AllowAnyOrigin()
            .AllowAnyHeader().AllowAnyMethod()
                           
            )
                           
                ;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
