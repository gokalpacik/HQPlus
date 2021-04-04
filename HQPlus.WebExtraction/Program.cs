using HQPlus.WebExtraction.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace HQPlus.WebExtraction
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();

            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            try
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Booking.html");

                string hotelInfo = serviceProvider
                    .GetService<IExtractorService>()
                    .ExtractDataFromHtml(filePath);

                Console.WriteLine(hotelInfo);
            }
            catch (Exception generalException)
            {
                var logger = serviceProvider.GetService<ILogger<Program>>();
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

            serviceCollection.AddTransient<IExtractorService, ExtractorService>();
        }
    }
}
