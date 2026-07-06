using Goods.BackOffice.Controllers.Infrastructure;
using Goods.Domain.Products;
using Goods.Domain.Services;
using Goods.Domain.TransportVehicles;
using Goods.Tools.Types.Results;
using Microsoft.AspNetCore.Mvc;

namespace Goods.BackOffice.Controllers.TransportVechiles
{
    public class TransportVechilesController(ITransportVechilesService transportVechilesService) : BaseController
    {
        [HttpGet("/transports")]
        public IActionResult Index() => ReactApp();

        [HttpPost("transports/save")]
        public Task<Result> SaveTransportVechiles([FromBody] TransportVechileBlank transportVechileBlank)
        {
            return transportVechilesService.SaveTransportVechile(transportVechileBlank);
        }

        [HttpGet("transports/get-page")]
        public Task<Page<TransportVechile>> GetTransportVechilesPage([FromQuery] Int32 page, [FromQuery] Int32 count)
        {
            return transportVechilesService.GetTransportVechiles(page, count);
        }

        [HttpGet("transports/get-by-id")]
        public Task<TransportVechile> GetTransportVechile([FromQuery] Guid id)
        {
            return transportVechilesService.GetTransportVechile(id);
        }

        [HttpGet("transports/remove")]
        public Task<Result> RemoveTransportVechile([FromQuery] Guid id)
        {
            return transportVechilesService.RemoveTransportVechile(id);
        }
    }
}
