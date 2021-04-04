using HQPlus.Application.Contracts.Persistence;
using HQPlus.Domain.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace HQPlus.Persistence.Repositories
{
    public class HotelWithRatesRepository : BaseRepository<HotelWithRates>, IHotelWithRatesRepository
    {
        public async Task<IEnumerable<HotelWithRates>> GetHotelWithRates(int? hotelId, DateTime? arrivalDate)
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "hotelrates.json");

            using StreamReader reader = new StreamReader(filePath);
            string json = await reader.ReadToEndAsync();

            var hotelWithRates = JsonSerializer.Deserialize<IEnumerable<HotelWithRates>>(json, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });

            if (hotelId.HasValue) hotelWithRates = hotelWithRates.Where(x => x.Hotel.HotelID == hotelId);

            if (arrivalDate.HasValue)
            {
                hotelWithRates = hotelWithRates
                .Select(s => new HotelWithRates
                 {
                     Hotel = s.Hotel,
                     HotelRates = s.HotelRates.Where(x => x.TargetDay.Date == arrivalDate.Value.Date)
                });
            }

            return hotelWithRates;
        }
    }
}
