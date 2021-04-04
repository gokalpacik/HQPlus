using HQPlus.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HQPlus.Application.Contracts.Persistence
{
    public interface IHotelWithRatesRepository : IAsyncRepository<HotelWithRates>
    {
        Task<IEnumerable<HotelWithRates>> GetHotelWithRates(int? hotelId, DateTime? arrivalDate);
    }
}
