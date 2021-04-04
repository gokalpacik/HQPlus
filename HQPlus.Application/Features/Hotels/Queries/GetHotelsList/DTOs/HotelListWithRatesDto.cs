using System.Collections.Generic;

namespace HQPlus.Application.Features.Hotels.Queries.GetHotelsList.DTOs
{
    public class HotelListWithRatesDto
    {
        public int HotelId { get; set; }
        public string Name { get; set; }
        public int Classification { get; set; }
        public double Reviewscore { get; set; }
        public IEnumerable<HotelRateDto> HotelRates { get; set; }

    }
}
