using Goods.Domain.Drivers;
using Goods.Domain.TransportVehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goods.Services.Drivers.Repositories
{
    public interface IDriversRepository
    {
        Task SaveDriver(Driver productBlank);
        Task<Driver?> GetDriver(Guid id);
        Task<Page<Driver>> GetDrivers(Int32 page, Int32 count);
        Task RemoveDriver(Guid id);
    }
}
