using Goods.Domain.TransportVehicles;
using Goods.Domain.TransportVehicles.Enums;
using Goods.Services.Products.Repositories.Models;
using Goods.Services.TransportVehicles.Repositories.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goods.Services.TransportVechiles.Repositories.Converters
{
    internal static class TransportVechileConverter
    {
        internal static TransportVechile ToTransportVechile(this TransportVechileDb transportVechileDb)
        {
            return new TransportVechile(
                transportVechileDb.Id,
                transportVechileDb.Type,
                transportVechileDb.Name,
                transportVechileDb.StateNumber,
                transportVechileDb.AverageSpeed,
                transportVechileDb.FuelConsumption,
                transportVechileDb.IsRemoved
            );
        }

        internal static TransportVechileDb ToTransportVechileDb(this NpgsqlDataReader reader)
        {
            return new TransportVechileDb(
                reader.GetGuid(reader.GetOrdinal("id")),
                (TransportVehiclesTypes)reader.GetInt32(reader.GetOrdinal("type")),
                reader.GetString(reader.GetOrdinal("name")),
                reader.GetString(reader.GetOrdinal("statenumber")),
                reader.GetDouble(reader.GetOrdinal("averagespeed")),
                reader.GetDouble(reader.GetOrdinal("fuelconsumption")),
                reader.GetDateTime(reader.GetOrdinal("createddatetimeutc")),
                reader.IsDBNull(reader.GetOrdinal("modifieddatetimeutc"))
                    ? null
                    : reader.GetDateTime(reader.GetOrdinal("modifieddatetimeutc")),
                reader.GetBoolean(reader.GetOrdinal("isremoved"))
            );
        }

        public static TransportVechileDb ToTransportVechileDb(this TransportVechile transportVechile)
        {
            return new TransportVechileDb(
                transportVechile.Id,
                transportVechile.Type,
                transportVechile.Name,
                transportVechile.StateNumber,
                transportVechile.AverageSpeed,
                transportVechile.FuelConsumption,
                createdDateTimeUtc: DateTime.UtcNow,
                modifiedDateTimeUtc: DateTime.UtcNow,
                isRemoved: false
            );
        }
    }
}
