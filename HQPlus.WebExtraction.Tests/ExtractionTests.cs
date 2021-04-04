using HQPlus.WebExtraction.Services;
using NUnit.Framework;
using System;
using System.IO;

namespace HQPlus.WebExtraction.Tests
{
    [TestFixture]
    public class ExtractionTests
    {
        private ExtractorService _extractorService;

        public ExtractionTests()
        {
            _extractorService = new ExtractorService();
        }

        [Test]
        public void SuccessIfFileIsExist()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Booking.html");
            var result = _extractorService.ExtractDataFromHtml(filePath);

            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
        }

        [Test]
        public void ErrorIfFileIsNotExist()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Booking1.html");

            Assert.Throws<FileNotFoundException>(() => _extractorService.ExtractDataFromHtml(filePath));            
        }

        [Test]
        public void ErrorIfFilePathIsNull()
        {        
            Assert.Throws<ArgumentNullException>(() => _extractorService.ExtractDataFromHtml(null));
        }
    }
}
