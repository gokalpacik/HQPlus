using System.Collections.Generic;

namespace HQPlus.Reporting.Models
{
    public class HotelWithRates
    {
        public Hotel Hotel { get; set; }
        public List<HotelRate> HotelRates { get; set; }
    }
}
