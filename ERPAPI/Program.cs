using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ERP.Contexts;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace ERPAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("Nlog.config").GetCurrentClassLogger();
            var bindingConfig = new ConfigurationBuilder()
             .AddCommandLine(args)
             .Build();

            var webHost = CreateWebHostBuilder(args)
                .Build();

            //para las migraciones tambien usar Package manager console Script-Migration -Idempotent
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var applicationDbContext = services.GetService<ApplicationDbContext>();
                    applicationDbContext.Database.Migrate();

            }

            webHost.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                   .ConfigureLogging(logging =>
                   {
                       logging.ClearProviders();
                       logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                   })
                .UseNLog()
                .UseStartup<Startup>();
    }
}
