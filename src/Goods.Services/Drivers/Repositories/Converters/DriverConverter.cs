using Goods.Domain.Drivers;
using Goods.Domain.Drivers.Enums;
using Goods.Domain.Products;
using Goods.Domain.TransportVehicles.Enums;
using Goods.Services.Drivers.Repositories.Models;
using Goods.Services.Products.Repositories.Models;
using Goods.Services.TransportVechiles.Repositories.Converters;
using Goods.Services.TransportVehicles.Repositories.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goods.Services.Drivers.Repositories.Converters
{
    internal static class DriverConverter
    {
        internal static Driver ToDriver(this DriverDb driverDb)
        {
            return new Driver(
                driverDb.Id,
                driverDb.Name,
                driverDb.Surname,
                driverDb.Patronymic,
                driverDb.Gender,
                driverDb.RightsCategories,
                driverDb.Age,
                driverDb.Experience,
                driverDb.TransportVechileDb.ToTransportVechile(),
                driverDb.Payment,
                driverDb.IsRemoved
            );
        }

        internal static DriverDb ToDriverDb(this NpgsqlDataReader reader)
        {
            TransportVechileDb transportVechileDb = new(
            reader.GetGuid(reader.GetOrdinal("vehicle_id")),
            (TransportVehiclesTypes)reader.GetInt32(reader.GetOrdinal("vehicle_type")),
            reader.GetString(reader.GetOrdinal("vehicle_name")),
            reader.GetString(reader.GetOrdinal("vehicle_statenumber")),
            reader.GetDouble(reader.GetOrdinal("vehicle_averagespeed")),
            reader.GetDouble(reader.GetOrdinal("vehicle_fuelconsumption")),
            reader.GetDateTime(reader.GetOrdinal("vehicle_createddatetimeutc")),
            reader.IsDBNull(reader.GetOrdinal("vehicle_modifieddatetimeutc"))
                ? null
                : reader.GetDateTime(reader.GetOrdinal("vehicle_modifieddatetimeutc")),
            reader.GetBoolean(reader.GetOrdinal("vehicle_isremoved"))
        );
            return new DriverDb(
                reader.GetGuid(reader.GetOrdinal("id")),
                reader.GetString(reader.GetOrdinal("name")),
                reader.GetString(reader.GetOrdinal("surname")),
                reader.IsDBNull(reader.GetOrdinal("patronymic")) ? null : reader.GetString(reader.GetOrdinal("patronymic")),
                (Gender)reader.GetInt32(reader.GetOrdinal("gender")),
                reader.GetFieldValue<Int32[]>(reader.GetOrdinal("rightscategories")).Select(x => (RightsCategory)x).ToArray(),
                reader.GetInt32(reader.GetOrdinal("age")),
                reader.GetInt32(reader.GetOrdinal("experience")),
                transportVechileDb,
                reader.GetDouble(reader.GetOrdinal("payment")),
                reader.GetDateTime(reader.GetOrdinal("createddatetimeutc")),
                reader.IsDBNull(reader.GetOrdinal("modifieddatetimeutc"))
                    ? null
                    : reader.GetDateTime(reader.GetOrdinal("modifieddatetimeutc")),
                reader.GetBoolean(reader.GetOrdinal("isremoved"))
            );
        }

        public static DriverDb ToDriverDb(this Driver driver)
        {
            return new DriverDb(
                driver.Id,
                driver.Name,
                driver.Surname,
                driver.Patronymic,
                driver.Gender,
                driver.RightsCategories,
                driver.Age,
                driver.Experience,
                driver.TransportVechile.ToTransportVechileDb(),
                driver.Payment,
                createdDateTimeUtc: DateTime.UtcNow,
                modifiedDateTimeUtc: DateTime.UtcNow,
                isRemoved: false
            );
        }
    }
}
