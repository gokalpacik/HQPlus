using System.Threading.Tasks;

namespace HQPlus.Reporting.Services
{
    public interface IReportingService
    {
        public Task CreateReportAsync(string filePath);
    }
}
