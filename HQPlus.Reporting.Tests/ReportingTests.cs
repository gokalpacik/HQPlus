using HQPlus.Reporting.Services;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;

namespace HQPlus.Reporting.Tests
{
    [TestFixture]
    public class ReportingTests
    {
        private ReportingService _reportingService;
        private string _filePath;

        public ReportingTests()
        {
            _reportingService = new ReportingService();
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "hotelrates.json");
        }

        [Test]
        public async Task SuccessIfFileIsExistAsync()
        {
            await _reportingService.CreateReportAsync(_filePath);

            Assert.IsTrue(true);
        }

        [Test]
        public void ErrorIfFileIsNotExist()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "hotelrates1.json");
             Assert.ThrowsAsync<FileNotFoundException>(async () => await _reportingService.CreateReportAsync(filePath));
        }

        [Test]
        public void ErrorIfFilePathIsNull()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async() => await _reportingService.CreateReportAsync(null));
        }

        [Test]
        public async Task IsExcelFileExistAfterCreatingReportAsync()
        {
            await _reportingService.CreateReportAsync(_filePath);

            string outputfilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Report-{ DateTime.UtcNow:dd_MM_yyyy}.xlsx");
            Assert.IsTrue(File.Exists(outputfilePath));
        }
    }
}
