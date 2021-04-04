using HQPlus.Api;
using HQPlus.Application.Features.Hotels.Queries.GetHotelsList.DTOs;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace HQPlus.API.IntegratrionTests
{
    [TestFixture]
    public class SampleControllerTests
    {
        private APIWebApplicationFactory _factory;
        private HttpClient _client;

        [OneTimeSetUp]
        public void GivenARequestToTheController()
        {
            _factory = new APIWebApplicationFactory();
            _client = _factory.CreateClient();
        }

        [Test]
        public async Task ShouldReturn200WhenHotelListWithRatesCalled()
        {            
            var response = await _client.GetAsync("/hotelListWithRates");
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Test]
        public async Task ShoulCorrectResultTypedWhenHotelListWithRatesCalled()
        {
            var response = await _client.GetAsync("/hotelListWithRates");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<HotelListWithRatesDto>>(responseString);

            result.ShouldBeOfType<List<HotelListWithRatesDto>>();
            result.ShouldNotBeNull();
        }


        [OneTimeTearDown]
        public void TearDown()
        {
            _client.Dispose();
            _factory.Dispose();
        }
    }
}
