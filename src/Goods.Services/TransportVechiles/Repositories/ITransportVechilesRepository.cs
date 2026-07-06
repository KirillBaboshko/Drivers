using Goods.Domain.TransportVehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goods.Services.TransportVechiles.Repositories
{
    public interface ITransportVechilesRepository
    {
        Task SaveTransportVechile(TransportVechile productBlank);
        Task<TransportVechile?> GetTransportVechile(Guid id);
        Task<TransportVechile?> GetTransportVechile(String name);
        Task<Page<TransportVechile>> GetTransportVechiles(Int32 page, Int32 count);
        Task RemoveTransportVechile(Guid id);
    }
}
