using Goods.Domain.Drivers;
using Goods.Services.Drivers.Repositories.Converters;
using Goods.Services.Drivers.Repositories.Models;
using Goods.Services.Drivers.Repositories.Queries;
using Goods.Tools.Utils;
using static Goods.Tools.Utils.NumberUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goods.Services.Drivers.Repositories
{
    public class DriversRepository: IDriversRepository
    {
        public Task SaveDriver(Driver driver)
        {
            DriverDb driverDb = driver.ToDriverDb();

            return DatabaseUtils.ExecuteAsync(
                Sql.Drivers_Save,
                parameters =>
                {
                    parameters.AddWithValue("@id", driverDb.Id);
                    parameters.AddWithValue("@name", driverDb.Name);
                    parameters.AddWithValue("@surname", driverDb.Surname);
                    parameters.AddWithValue("@patronymic", (Object?)driverDb.Patronymic ?? DBNull.Value);
                    parameters.AddWithValue("@gender", (Int32)driverDb.Gender);
                    parameters.AddWithValue("@rightsCategories", driverDb.RightsCategories.Select(x => (Int32)x).ToArray());
                    parameters.AddWithValue("@age", driverDb.Age);
                    parameters.AddWithValue("@experience", driverDb.Experience);
                    parameters.AddWithValue("@transportVehicleId", driverDb.TransportVechileDb.Id);
                    parameters.AddWithValue("@payment", driverDb.Payment);
                    parameters.AddWithValue("@createdDateTimeUtc", driverDb.CreatedDateTimeUtc);
                    parameters.AddWithValue("@modifiedDateTimeUtc", (Object?)driverDb.ModifiedDateTimeUtc ?? DBNull.Value);
                    parameters.AddWithValue("@isRemoved", driverDb.IsRemoved);
                }
            );
        }

        public async Task<Driver?> GetDriver(Guid id)
        {
            DriverDb? driverDb = await DatabaseUtils.GetAsync<DriverDb?>(
                Sql.Drivers_GetById,
                parameters =>
                {
                    parameters.AddWithValue("@id", id);
                },
                reader => reader.ToDriverDb()
            );

            return driverDb?.ToDriver();
        }

        public async Task<Page<Driver>> GetDrivers(Int32 page, Int32 count)
        {
            (Int32 offset, Int32 limit) = NormalizeRange(page, count);

            Page<DriverDb> pageDb = await DatabaseUtils.GetPageAsync(
                Sql.Drivers_GetPage,
                parameters =>
                {
                    parameters.AddWithValue("@offset", offset);
                    parameters.AddWithValue("@limit", limit);
                },
                reader => reader.ToDriverDb()
            );

            return pageDb.Convert(driverDb => driverDb.ToDriver());
        }

        public Task RemoveDriver(Guid id)
        {
            return DatabaseUtils.ExecuteAsync(
                Sql.Drivers_Remove,
                parameters =>
                {
                    parameters.AddWithValue("@id", id);
                    parameters.AddWithValue("@modifiedDateTimeUtc", DateTime.UtcNow);
                }
            );
        }
    }
}
