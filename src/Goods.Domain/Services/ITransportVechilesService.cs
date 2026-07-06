using Goods.Domain.Drivers;
using Goods.Domain.TransportVehicles;
using Goods.Tools.Types.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goods.Domain.Services
{
    public interface ITransportVechilesService
    {
        Task<Result> SaveTransportVechile(TransportVechileBlank productBlank);
        Task<TransportVechile> GetTransportVechile(Guid id);
        Task<Page<TransportVechile>> GetTransportVechiles(Int32 page, Int32 count);
        Task<Result> RemoveTransportVechile(Guid id);
    }
}
