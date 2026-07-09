using Goods.Domain.Drivers;
using Goods.Domain.Drivers.Enums;
using Goods.Domain.Services;
using Goods.Domain.TransportVehicles;
using Goods.Services.Drivers.Repositories;
using Goods.Domain.TransportVehicles.Enums;
using Goods.Services.TransportVechiles;
using Goods.Tools.Types.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata.Ecma335;

namespace Goods.Services.Drivers
{
    public class DriversService(IDriversRepository driversRepository, ITransportVechilesService transportVechilesService) : IDriversService
    {
        
        public async Task<Result> SaveDriver(DriverBlank driverBlank)
        {
            DataResult<Driver> validationResult = await ValidateDriverBlank(driverBlank);
            if (validationResult.IsFail(out Driver driver)) return validationResult.ToResult();

            await driversRepository.SaveDriver(driver);

            return Result.Success();
        }

        #region Validation

        private async Task<DataResult<Driver>> ValidateDriverBlank(DriverBlank driverBlank)
        {
            DataResult<Guid?> existDriverValidationResult = await ValidateExistDriver(driverBlank);
            if (existDriverValidationResult.IsFail(out Guid? id)) return DataResult<Driver>.Fail(existDriverValidationResult);

            DataResult<String> nameValidationResult = await ValidateDriverName(driverBlank);
            if (nameValidationResult.IsFail(out String name)) return DataResult<Driver>.Fail(nameValidationResult);

            DataResult<String> surnameValidationResult = await ValidateDriverSurname(driverBlank);
            if (nameValidationResult.IsFail(out String surname)) return DataResult<Driver>.Fail(surnameValidationResult);

            DataResult<Gender> genderValidationResult = ValidateDriverGender(driverBlank);
            if (genderValidationResult.IsFail(out Gender gender)) return DataResult<Driver>.Fail(genderValidationResult);

            DataResult<RightsCategory[]> rightsValidationResult = ValidateRightsCategories(driverBlank);
            if (rightsValidationResult.IsFail(out RightsCategory[] rightsCategories)) return DataResult<Driver>.Fail(rightsValidationResult);

            DataResult<Int32> ageValidationResult = ValidateDriverAge(driverBlank);
            if (ageValidationResult.IsFail(out Int32 age)) return DataResult<Driver>.Fail(ageValidationResult);

            DataResult<Int32> experienceValidationResult = ValidateDriverExperience(driverBlank);
            if (experienceValidationResult.IsFail(out Int32 experience)) return DataResult<Driver>.Fail(experienceValidationResult);

            DataResult<Double> paymentValidationResult = ValidatePayment(driverBlank);
            if (paymentValidationResult.IsFail(out Double payment)) return DataResult<Driver>.Fail(paymentValidationResult);

            DataResult<TransportVechile> transportVechileValidationResult =
            await ValidateTransportVechile(driverBlank, rightsCategories, age, experience);
            if (transportVechileValidationResult.IsFail(out TransportVechile transportVechile))
                return DataResult<Driver>.Fail(transportVechileValidationResult);

            Driver driver = new(
                id ?? Guid.NewGuid(),
                name,
                surname,
                driverBlank.Patronymic,
                gender,
                rightsCategories,
                age,
                experience,
                transportVechile,
                payment,
                isRemoved: false
            );

            return DataResult<Driver>.Success(driver);
        }

        private async Task<DataResult<Guid?>> ValidateExistDriver(DriverBlank driverBlank)
        {
            if (driverBlank.Id is not { } id)
                return DataResult<Guid?>.Success(null);

            Driver existDriver = await GetDriver(id);
            if (existDriver.IsRemoved) return DataResult<Guid?>.Fail("Водитель удален");

            return DataResult<Guid?>.Success(id);
        }

        private async Task<DataResult<String>> ValidateDriverName(DriverBlank driverBlank)
        {
            if (String.IsNullOrWhiteSpace(driverBlank.Name))
                return DataResult<String>.Fail("Не указано имя водителя");

            const Int32 maxDriverNameLength = 255;
            if (driverBlank.Name.Length >= maxDriverNameLength)
                return DataResult<String>.Fail($"Имя водителя слишком длинное. Максимально допустимо {maxDriverNameLength} символов");

            return DataResult<String>.Success(driverBlank.Name);
        }
        private async Task<DataResult<String>> ValidateDriverSurname(DriverBlank driverBlank)
        {
            if (String.IsNullOrWhiteSpace(driverBlank.Surname))
                return DataResult<String>.Fail("Не указана фамилия водителя");

            const Int32 maxDriverNameLength = 255;
            if (driverBlank.Surname.Length >= maxDriverNameLength)
                return DataResult<String>.Fail($"Фамилия водителя слишком длинная. Максимально допустимо {maxDriverNameLength} символов");

            return DataResult<String>.Success(driverBlank.Surname);
        }
        private DataResult<Gender> ValidateDriverGender(DriverBlank driverBlank)
        {
            if (driverBlank.Gender is not { } gender)
                return DataResult<Gender>.Fail("Выберите пол водителя");

            if (!Enum.IsDefined(gender))
                throw new Exception($"Пол {gender} не существует");

            return DataResult<Gender>.Success(gender);
        }
        private DataResult<RightsCategory[]> ValidateRightsCategories(DriverBlank driverBlank)
        {
            if (driverBlank.RightsCategories is not { Length: > 0 } rightsCategories)
                return DataResult<RightsCategory[]>.Fail("Выберите хотя бы одну категорию прав");

            foreach (RightsCategory rightsCategory in rightsCategories)
                if (!Enum.IsDefined(rightsCategory))
                    throw new Exception($"Категория прав {rightsCategory} не существует");

            return DataResult<RightsCategory[]>.Success(rightsCategories.Distinct().ToArray());
        }

        private DataResult<Int32> ValidateDriverAge(DriverBlank driverBlank)
        {
            if (driverBlank.Age is not { } age)
                return DataResult<Int32>.Fail("Не указан возраст водителя");

            const Int32 minAge = 18;
            const Int32 maxAge = 100;
            if (age < minAge || age > maxAge)
                return DataResult<Int32>.Fail($"Возраст должен быть в диапазоне от {minAge} до {maxAge} лет");

            return DataResult<Int32>.Success(age);
        }
        private DataResult<Int32> ValidateDriverExperience(DriverBlank driverBlank)
        {
            const Int32 minAge = 18;
            if (driverBlank.Experience is not { } experience)
                return DataResult<Int32>.Fail("Не указан стаж водителя");

            if (experience < 0)
                return DataResult<Int32>.Fail("Стаж не может быть отрицательным");

            if (experience > (driverBlank.Age- minAge))
                return DataResult<Int32>.Fail("Стаж до получения прав не учитывается");

            return DataResult<Int32>.Success(experience);
        }
        private DataResult<Double> ValidatePayment(DriverBlank driverBlank)
        {
            if (driverBlank.Payment is not { } payment)
                return DataResult<Double>.Fail("Не указана оплата в час водителя");

            if (payment < 0)
                return DataResult<Double>.Fail("Оплата в час не может быть отрицательным");

            return DataResult<Double>.Success(payment);
        }
        private async Task<DataResult<TransportVechile>> ValidateTransportVechile(
            DriverBlank driverBlank,
            RightsCategory[] rightsCategories,
            Int32 age,
            Int32 experience)
        {
            if (driverBlank.TransportVechileId is not { } transportVechileId)
                return DataResult<TransportVechile>.Fail("Не указан транспорт водителя");
            TransportVechile transportVechile = await transportVechilesService.GetTransportVechile(transportVechileId);
            if (transportVechile.IsRemoved)
                return DataResult<TransportVechile>.Fail("Выбранное транспортное средство удалено");
            RightsCategory requiredCategory = GetRightsCategory(transportVechile.Type);
            if (!rightsCategories.Contains(requiredCategory))
                return DataResult<TransportVechile>.Fail(
                    $"Для управления «{transportVechile.Name}» требуется категория прав «{GetRightsCategoryName(requiredCategory)}»");
            if(transportVechile.Type==TransportVehiclesTypes.Bus)
            {
                const Int32 minAge = 21;
                const Int32 minExperince =3;
                if (age<minAge) return DataResult<TransportVechile>.Fail(
                    $"Для управления «{transportVechile.Name}» водитель должен быть не младше {minAge} года»");
                if (experience < minExperince) return DataResult<TransportVechile>.Fail(
                   $"Для управления «{transportVechile.Name}» стаж водителя должен быть не менее {minExperince} лет»");
            }
            return DataResult<TransportVechile>.Success(transportVechile);
        }

        private static RightsCategory GetRightsCategory(TransportVehiclesTypes type)
        {
            return type switch
            {
                TransportVehiclesTypes.PassengerCar => RightsCategory.CategoryB,
                TransportVehiclesTypes.Truck => RightsCategory.CategoryC,
                TransportVehiclesTypes.Bus => RightsCategory.CategoryD,
                _ => throw new Exception($"Неизвестный тип транспортного средства {type}")
            };
        }

        private static String GetRightsCategoryName(RightsCategory category)
        {
            return category switch
            {
                RightsCategory.CategoryM => "M",
                RightsCategory.CategoryA => "A",
                RightsCategory.CategoryB => "B",
                RightsCategory.CategoryC => "C",
                RightsCategory.CategoryD => "D",
                _ => category.ToString()
            };
        }
        #endregion Validation

        public async Task<Driver> GetDriver(Guid driverId)
        {
            Driver? driver = await driversRepository.GetDriver(driverId);
            if (driver is null) throw new Exception($"Водитель {driverId} не найден");

            return driver;
        }


        public Task<Page<Driver>> GetDrivers(Int32 page, Int32 countInPage)
        {
            return driversRepository.GetDrivers(page, countInPage);
        }

        public async Task<Result> RemoveDriver(Guid id)
        {
            Driver driver = await GetDriver(id);
            if (driver.IsRemoved) return Result.Fail("Водитель уже удален");

            await driversRepository.RemoveDriver(id);

            return Result.Success();
        }
        public async Task<DataResult<TripCost>> GetTripCost(Guid id)
        {
            Driver driver = await GetDriver(id);
            return DataResult<TripCost>.Success(TripCost.Calculate(driver)); 
        }
        
        public async Task<DataResult<Decimal>> GetTripCostPeriod(Guid id,DateOnly startDay, DateOnly endDay)
        {
            Driver driver = await GetDriver(id);
            Double resultsum = 0;
            DateOnly day = startDay;
            while (day.DayNumber<=endDay.DayNumber)
            {
                resultsum += TripCost.Calculate(driver,day).ResultPrice;
                day.AddDays(1);
            }
            return DataResult<Decimal>.Success((Decimal)resultsum);

        }
    }
    

}
