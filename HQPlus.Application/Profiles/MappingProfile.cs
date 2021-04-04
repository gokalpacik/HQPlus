using AutoMapper;
using HQPlus.Application.Features.Hotels.Queries.GetHotelsList.DTOs;
using HQPlus.Domain.Entity;

namespace HQPlus.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<HotelWithRates, HotelListWithRatesDto>()
                .ForMember(dest => dest.HotelId, opt => opt.MapFrom(src => src.Hotel.HotelID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Hotel.Name))
                .ForMember(dest => dest.Classification, opt => opt.MapFrom(src => src.Hotel.Classification))
                .ForMember(dest => dest.Reviewscore, opt => opt.MapFrom(src => src.Hotel.Reviewscore))
                .ReverseMap();

            CreateMap<HotelRate, HotelRateDto>()
                .ForMember(dest => dest.LengthOfStay, opt => opt.MapFrom(src => src.Los))
                .ForMember(dest => dest.ArrivalDate, opt => opt.MapFrom(src => src.TargetDay))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.NumericFloat))
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Price.Currency))
               .ReverseMap();

            CreateMap<Rate, RateDto>()               
              .ReverseMap();
        }
    }
}
