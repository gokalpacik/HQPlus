using HQPlus.Application.Features.Hotels.Queries.GetHotelsList.DTOs;
using MediatR;
using System;
using System.Collections.Generic;

namespace HQPlus.Application.Features.Hotels.Queries.GetHotelsList
{
    public class GetHotelListWithRatesQuery : IRequest<List<HotelListWithRatesDto>>
    {
        public int? HotelId { get; set; }
        public DateTime? ArrivalDate { get; set; }
    }
}
