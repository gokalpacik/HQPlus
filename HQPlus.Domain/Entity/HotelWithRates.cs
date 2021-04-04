using System.Collections.Generic;

namespace HQPlus.Domain.Entity
{
    public class HotelWithRates
    {
        public Hotel Hotel { get; set; }
        public IEnumerable<HotelRate> HotelRates { get; set; }
    }
}
