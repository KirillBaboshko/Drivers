using Goods.BackOffice.Controllers.Infrastructure;
using Goods.Domain.Drivers;
using Goods.Domain.Drivers;
using Goods.Domain.Services;
using Goods.Services.Drivers;
using Goods.Services.Drivers.Repositories.Models;
using Goods.Tools.Types.Results;
using Microsoft.AspNetCore.Mvc;

namespace Goods.BackOffice.Controllers.Drivers
{
    public class DriversController(IDriversService driversService) : BaseController
    {
        [HttpGet("/drivers")]
        public IActionResult Index() => ReactApp();

        [HttpPost("drivers/save")]
        public Task<Result> SaveDrivers([FromBody] DriverBlank driverBlank)
        {
            return driversService.SaveDriver(driverBlank);
        }

        [HttpGet("drivers/get-page")]
        public Task<Page<Driver>> GetDriversPage([FromQuery] Int32 page, [FromQuery] Int32 count)
        {
            return driversService.GetDrivers(page, count);
        }

        [HttpGet("drivers/get-by-id")]
        public Task<Driver> GetDriver([FromQuery] Guid id)
        {
            return driversService.GetDriver(id);
        }


        [HttpGet("drivers/remove")]
        public Task<Result> RemoveDriver([FromQuery] Guid id)
        {
            return driversService.RemoveDriver(id);
        }
        [HttpGet("drivers/trip-cost")]
        public Task<DataResult<TripCost>> GetTripCost([FromQuery] Guid id)
        {
            return driversService.GetTripCost(id);
        }
    }
}
