using AutoMapper;
using HQPlus.Application.Contracts.Persistence;
using HQPlus.Application.Features.Hotels.Queries.GetHotelsList;
using HQPlus.Application.Features.Hotels.Queries.GetHotelsList.DTOs;
using HQPlus.Application.Profiles;
using HQPlus.Domain.Entity;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HQPlus.Application.Tests
{
    [TestFixture]
    public class ApplicationTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IHotelWithRatesRepository> _mockHotelWithRatesRepository;

        public ApplicationTests()
        {
            _mockHotelWithRatesRepository = new Mock<IHotelWithRatesRepository>();

            var hotelWithRates = new HotelWithRates
            {
                Hotel = new Hotel
                {
                    HotelID = 1,
                    Name = "Çırağan Kampinski Palace",
                    Classification = 5,
                    Reviewscore = 5
                },
                HotelRates = new List<HotelRate>()
            };

            List<HotelWithRates> hotelWithRatesList = new List<HotelWithRates>
            {
                hotelWithRates
            };

            _mockHotelWithRatesRepository.Setup(x => x.GetHotelWithRates(It.IsAny<int>(), It.IsAny<DateTime>())).ReturnsAsync(hotelWithRatesList);

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Test]
        public async Task GetHotelListWithRatesShouldBeTypeOfTest()
        {
            var handler = new GetHotelListWithRatesQueryHandler(_mapper, _mockHotelWithRatesRepository.Object);

            var result = await handler.Handle(new GetHotelListWithRatesQuery{ HotelId = 1, ArrivalDate = DateTime.Now } , CancellationToken.None);
            result.ShouldBeOfType<List<HotelListWithRatesDto>>();
        }

        [Test]
        public async Task GetHotelListWithRatesCountTest()
        {
            var handler = new GetHotelListWithRatesQueryHandler(_mapper, _mockHotelWithRatesRepository.Object);

            var result = await handler.Handle(new GetHotelListWithRatesQuery { HotelId = 1, ArrivalDate = DateTime.Now }, CancellationToken.None);
            result.Count.ShouldBe(1);
        }

        [Test]
        public async Task GetHotelListWithRatesHotelNameTest()
        {
            var handler = new GetHotelListWithRatesQueryHandler(_mapper, _mockHotelWithRatesRepository.Object);

            var result = await handler.Handle(new GetHotelListWithRatesQuery { HotelId = 1, ArrivalDate = DateTime.Now }, CancellationToken.None);

            result.FirstOrDefault()?.Name.StartsWith("Çırağan");
        }
    }
}
