using System;
using System.Collections.Generic;

namespace HQPlus.Application.Features.Hotels.Queries.GetHotelsList.DTOs
{
    public class HotelRateDto
    {
        public int Adults { get; set; }
        public int LengthOfStay { get; set; }
        public double Price { get; set; }
        public string Currency { get; set; }
        public string RateDescription { get; set; }
        public string RateID { get; set; }
        public string RateName { get; set; }
        public IEnumerable<RateDto> RateTags { get; set; }
        public DateTime ArrivalDate { get; set; }
    }
}
