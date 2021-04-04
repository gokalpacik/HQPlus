using System;
using System.Collections.Generic;
using System.Text;

namespace HQPlus.Domain.Entity
{
    public class HotelRate
    {
        public int Adults { get; set; }
        public int Los { get; set; }
        public Price Price { get; set; }
        public string RateDescription { get; set; }
        public string RateID { get; set; }
        public string RateName { get; set; }
        public IEnumerable<Rate> RateTags { get; set; }
        public DateTime TargetDay { get; set; }        
    }
}
