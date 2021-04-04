using HQPlus.Reporting.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace HQPlus.Reporting
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();

            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<Program>>();

            try
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "hotelrates.json");

                await serviceProvider
                    .GetService<IReportingService>()
                    .CreateReportAsync(filePath);

                logger.LogInformation($"Excel file has been created in {AppDomain.CurrentDomain.BaseDirectory} folder");
            }
            catch (Exception generalException)
            {
                logger.LogError(generalException, "An exception happened while running the service.");
            }

            Console.ReadKey();
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddLogging(opt =>
            {
                opt.AddConsole();
            });

            serviceCollection.AddTransient<IReportingService, ReportingService>();
        }
    }
}
