using HQPlus.WebExtraction.Extensions;
using HQPlus.WebExtraction.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace HQPlus.WebExtraction.Services
{
    public class ExtractorService : IExtractorService
    {
        public string ExtractDataFromHtml(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException(nameof(filePath));

            var htmlDoc = new HtmlDocument();
            htmlDoc.Load(filePath);

            Hotel hotel = ExtractHotelData(htmlDoc);

            return JsonSerializer.Serialize(hotel, new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });
        }

        private Hotel ExtractHotelData(HtmlDocument htmlDoc)
        {
            string hotelName = htmlDoc.DocumentNode.SelectSingleNode("//span[@id='hp_hotel_name']").InnerText.RemoveNewLineCharacter();
            string address = htmlDoc.DocumentNode.SelectSingleNode("//span[@id='hp_address_subtitle']").InnerText.RemoveNewLineCharacter();
            string stars = htmlDoc.DocumentNode.SelectSingleNode("//i[contains(@class, 'b-sprite stars')]")
                .Attributes
                .ToList()
                .FirstOrDefault(x => x.Name == "class")?
                .Value
                .GetStarCount();
            string reviewPoints = htmlDoc.DocumentNode.SelectSingleNode("//div[@id='reviewFloater']//span[contains(@class, 'average')]").InnerText.RemoveNewLineCharacter();
            string numberOfReviews = htmlDoc.DocumentNode.SelectSingleNode("//div[@id='reviewFloater']//span[contains(@class, 'score_from_number_of_reviews')]")
                .ChildNodes
                .FirstOrDefault(x => x.Name == "strong")?
                .InnerText
                .RemoveNewLineCharacter();

            var sb = new StringBuilder();
            htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'hotel_description_wrapper_exp')]//p").ToList().ForEach(x => sb.Append(x.InnerText.RemoveNewLineCharacter()));
            string description = sb.ToString();

            List<string> roomTypes = new List<string>();
            htmlDoc.DocumentNode.SelectNodes("//tbody//td[contains(@class, 'ftd')]").ToList().ForEach(x => roomTypes.Add(x.InnerText.RemoveNewLineCharacter()));

            List<string> alternativeHotels = new List<string>();
            htmlDoc.DocumentNode.SelectNodes("//ul[@id='js--lastViewedList']//li//a")
                .Where(x => x.ChildNodes.All(x => x.Name == "#text"))
                .ToList().ForEach(x => alternativeHotels.Add(x.InnerText.RemoveNewLineCharacter()));

            return new Hotel
            {
                HotelName = hotelName,
                Address = address,
                Stars = stars,
                ReviewPoints = reviewPoints,
                NumberOfReviews = numberOfReviews,
                Description = description,
                RoomTypes = roomTypes,
                AlternativeHotels = alternativeHotels
            };
        }
    }
}
