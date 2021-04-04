using System.Collections.Generic;

namespace HQPlus.WebExtraction.Models
{
    public class Hotel
    {
        public string HotelName { get; set; }
        public string Address { get; set; }
        public string Stars { get; set; }
        public string ReviewPoints { get; internal set; }
        public string NumberOfReviews { get; internal set; }
        public string Description { get; internal set; }
        public List<string> RoomTypes { get; set; }
        public List<string> AlternativeHotels { get; set; }
    }
}
