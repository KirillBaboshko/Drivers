using Goods.Domain.TransportVehicles;
using Goods.Services.TransportVehicles.Repositories.Models;
using Goods.Services.TransportVechiles.Repositories.Converters;
using Goods.Tools.Utils;
using static Goods.Tools.Utils.NumberUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goods.Services.TransportVehicles.Repositories.Queries;

namespace Goods.Services.TransportVechiles.Repositories
{
    public class TransportVechilesRepository : ITransportVechilesRepository
    {
        public Task SaveTransportVechile(TransportVechile transportVechile)
        {
            TransportVechileDb transportVechileDb = transportVechile.ToTransportVechileDb();

            return DatabaseUtils.ExecuteAsync(
                Sql.TransportVechiles_Save,
                parameters =>
                {
                    parameters.AddWithValue("@id", transportVechileDb.Id);
                    parameters.AddWithValue("@type", (Int32)transportVechileDb.Type);
                    parameters.AddWithValue("@name", transportVechileDb.Name);
                    parameters.AddWithValue("@stateNumber", transportVechileDb.StateNumber);
                    parameters.AddWithValue("@averageSpeed", transportVechileDb.AverageSpeed);
                    parameters.AddWithValue("@fuelConsumption", transportVechileDb.FuelConsumption);
                    parameters.AddWithValue("@createdDateTimeUtc", transportVechileDb.CreatedDateTimeUtc);
                    parameters.AddWithValue("@modifiedDateTimeUtc", (Object?)transportVechileDb.ModifiedDateTimeUtc ?? DBNull.Value);
                    parameters.AddWithValue("@isRemoved", transportVechileDb.IsRemoved);
                }
            );
        }

        public async Task<TransportVechile?> GetTransportVechile(Guid id)
        {
            TransportVechileDb? transportVechileDb = await DatabaseUtils.GetAsync<TransportVechileDb?>(
                Sql.TransportVechiles_GetById,
                parameters =>
                {
                    parameters.AddWithValue("@id", id);
                },
                reader => reader.ToTransportVechileDb()
            );

            return transportVechileDb?.ToTransportVechile();
        }

        public async Task<TransportVechile?> GetTransportVechile(String name)
        {
            TransportVechileDb? transportVechileDb = await DatabaseUtils.GetAsync<TransportVechileDb?>(
                Sql.TransportVechiles_GetByName,
                parameters =>
                {
                    parameters.AddWithValue("@name", name);
                },
                reader => reader.ToTransportVechileDb()
            );

            return transportVechileDb?.ToTransportVechile();
        }

        public async Task<Page<TransportVechile>> GetTransportVechiles(Int32 page, Int32 count)
        {
            (Int32 offset, Int32 limit) = NormalizeRange(page, count);

            Page<TransportVechileDb> pageDb = await DatabaseUtils.GetPageAsync(
                Sql.TransportVechiles_GetPage,
                parameters =>
                {
                    parameters.AddWithValue("@offset", offset);
                    parameters.AddWithValue("@limit", limit);
                },
                reader => reader.ToTransportVechileDb()
            );

            return pageDb.Convert(transportVechileDb => transportVechileDb.ToTransportVechile());
        }

        public Task RemoveTransportVechile(Guid id)
        {
            return DatabaseUtils.ExecuteAsync(
                Sql.TransportVechiles_Remove,
                parameters =>
                {
                    parameters.AddWithValue("@id", id);
                    parameters.AddWithValue("@modifiedDateTimeUtc", DateTime.UtcNow);
                }
            );
        }
    }
}