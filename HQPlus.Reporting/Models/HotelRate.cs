using System;
using System.Collections.Generic;

namespace HQPlus.Reporting.Models
{
    public class HotelRate
    {
        public int Adults { get; set; }
        public int Los { get; set; }
        public Price Price { get; set; }
        public string RateDescription { get; set; }
        public string RateID { get; set; }
        public string RateName { get; set; }
        public List<Rate> RateTags { get; set; }
        public DateTime TargetDay { get; set; }

        public DateTime ArrivalDate => TargetDay;
        public DateTime DepartureDate => TargetDay.AddDays(Los);
    }
}
