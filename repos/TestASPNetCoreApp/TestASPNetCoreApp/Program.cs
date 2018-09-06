using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace TestASPNetCoreApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //basic implementation that uses Nlog.config file
            var logger = NLogBuilder.
                ConfigureNLog("Nlog.config").GetCurrentClassLogger();

            //Logger class is a wrapper class that implements Ilogger interface
            //var logger = Logger.Logger.GetCurrentClassLogger<Program>();

            try
            {
                //logger.LogDebug("Init main method");
                logger.Debug("Init main method");
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                //logger.LogError(ex, $"The program stopped because of exception {ex.Message}");
                logger.Error(ex, $"The program stopped because of exception {ex.Message}");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Warning);
                })
                .UseNLog();
    }
}
