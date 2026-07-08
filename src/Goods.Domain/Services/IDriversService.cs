using Goods.Domain.Drivers;
using Goods.Domain.Products;
using Goods.Tools.Types.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goods.Domain.Services
{
    public interface IDriversService
    {
        Task<Result> SaveDriver(DriverBlank productBlank);
        Task<Driver> GetDriver(Guid id);
        Task<Page<Driver>> GetDrivers(Int32 page, Int32 count);
        Task<Result> RemoveDriver(Guid id);
        Task<DataResult<TripCost>> GetTripCost(Guid id);
    }
}
