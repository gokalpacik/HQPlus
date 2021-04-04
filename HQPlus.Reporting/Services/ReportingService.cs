using HQPlus.Reporting.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace HQPlus.Reporting.Services
{
    public class ReportingService : IReportingService
    {
        public async Task CreateReportAsync(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException(nameof(filePath));

            using StreamReader reader = new StreamReader(filePath);
            string json = await reader.ReadToEndAsync();

            var hotelWithRates = JsonSerializer.Deserialize<HotelWithRates>(json, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });

            ExportToExcel(hotelWithRates);
        }

        private static void ExportToExcel(HotelWithRates hotelWithRates)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excel = new ExcelPackage();

            var workSheet = excel.Workbook.Worksheets.Add("HotelRates");

            workSheet.TabColor = System.Drawing.Color.Black;
            workSheet.DefaultRowHeight = 12;
            workSheet.DefaultColWidth = 30;

            workSheet.Row(1).Height = 20;
            workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Row(1).Style.Font.Bold = true;

            workSheet.Cells[1, 1].Value = "ARRIVAL_DATE";
            workSheet.Cells[1, 2].Value = "DEPARTURE_DATE";
            workSheet.Cells[1, 3].Value = "PRICE";
            workSheet.Cells[1, 4].Value = "CURRENCY";
            workSheet.Cells[1, 5].Value = "RATENAME";
            workSheet.Cells[1, 6].Value = "ADULTS";
            workSheet.Cells[1, 7].Value = "BREAKFAST_INCLUDED";

            int recordIndex = 2;
            foreach (var hotelRate in hotelWithRates.HotelRates)
            {
                workSheet.Cells[recordIndex, 1].Value = hotelRate.ArrivalDate;
                workSheet.Cells[recordIndex, 1].Style.Numberformat.Format = "dd.MM.yy";
                workSheet.Cells[recordIndex, 2].Value = hotelRate.DepartureDate;
                workSheet.Cells[recordIndex, 2].Style.Numberformat.Format = "dd.MM.yy";
                workSheet.Cells[recordIndex, 3].Value = hotelRate.Price.NumericFloat;
                workSheet.Cells[recordIndex, 4].Value = hotelRate.Price.Currency;
                workSheet.Cells[recordIndex, 5].Value = hotelRate.RateName;
                workSheet.Cells[recordIndex, 6].Value = hotelRate.Adults;

                var shape = hotelRate.RateTags.FirstOrDefault();
                if (shape != null)
                {
                    workSheet.Cells[recordIndex, 7].Value = shape.Shape ? "1" : "0";
                }

                recordIndex++;
            }

            WriteToExcelFile(excel);

            excel.Dispose();
        }

        private static void WriteToExcelFile(ExcelPackage excel)
        {
            string outputfilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Report-{ DateTime.UtcNow:dd_MM_yyyy}.xlsx");

            if (File.Exists(outputfilePath))
                File.Delete(outputfilePath);

            FileStream fileStream = File.Create(outputfilePath);
            fileStream.Close();

            File.WriteAllBytes(outputfilePath, excel.GetAsByteArray());
        }
    }
}
