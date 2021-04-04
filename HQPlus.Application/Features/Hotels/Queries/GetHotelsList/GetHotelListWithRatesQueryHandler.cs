using AutoMapper;
using HQPlus.Application.Contracts.Persistence;
using HQPlus.Application.Features.Hotels.Queries.GetHotelsList.DTOs;
using HQPlus.Domain.Entity;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HQPlus.Application.Features.Hotels.Queries.GetHotelsList
{

    public class GetHotelListWithRatesQueryHandler : IRequestHandler<GetHotelListWithRatesQuery, List<HotelListWithRatesDto>>
    {
        private readonly IHotelWithRatesRepository _hotelWithRatesRepository;
        private readonly IMapper _mapper;

        public GetHotelListWithRatesQueryHandler(IMapper mapper, IHotelWithRatesRepository hotelWithRatesRepository)
        {
            _mapper = mapper;
            _hotelWithRatesRepository = hotelWithRatesRepository;
        }

        public async Task<List<HotelListWithRatesDto>> Handle(GetHotelListWithRatesQuery request, CancellationToken cancellationToken)
        {
            var hotelsWithRates = (await _hotelWithRatesRepository.GetHotelWithRates(request.HotelId, request.ArrivalDate));
            return _mapper.Map<List<HotelListWithRatesDto>>(hotelsWithRates);
        }
    }
}
