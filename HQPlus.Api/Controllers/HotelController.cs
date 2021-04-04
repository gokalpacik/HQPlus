using HQPlus.Application.Features.Hotels.Queries.GetHotelsList;
using HQPlus.Application.Features.Hotels.Queries.GetHotelsList.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace GloboTicket.TicketManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<HotelController> _logger;

        public HotelController(IMediator mediator, ILogger<HotelController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("/hotelListWithRates", Name = "GetHotelListWithRates")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<HotelListWithRatesDto>> GetHotelListWithRates(int? hotelId, DateTime? arrivalDate)
        {
            var getHotelListWithRatesQuery = new GetHotelListWithRatesQuery {  HotelId = hotelId, ArrivalDate = arrivalDate };
            var dtos = await _mediator.Send(getHotelListWithRatesQuery);

            _logger.LogInformation($"{dtos.Count} hotel/hotels found");

            return Ok(dtos);
        }
    }
}
