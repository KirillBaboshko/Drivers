using Goods.Domain.Services;
using Goods.Domain.TransportVehicles;
using Goods.Domain.TransportVehicles.Enums;
using Goods.Services.TransportVechiles.Repositories;
using Goods.Tools.Types.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Goods.Services.TransportVechiles
{
    public class TransportVechilesService(ITransportVechilesRepository transportVechilesRepository) : ITransportVechilesService
    {
        public async Task<Result> SaveTransportVechile(TransportVechileBlank transportVechileBlank)
        {
            DataResult<TransportVechile> validationResult = await ValidateTransportVechileBlank(transportVechileBlank);
            if (validationResult.IsFail(out TransportVechile transportVechile)) return validationResult.ToResult();

            await transportVechilesRepository.SaveTransportVechile(transportVechile);

            return Result.Success();
        }

        #region Validation

        private async Task<DataResult<TransportVechile>> ValidateTransportVechileBlank(TransportVechileBlank transportVechileBlank)
        {
            DataResult<Guid?> existValidationResult = await ValidateExistTransportVechile(transportVechileBlank);
            if (existValidationResult.IsFail(out Guid? id)) return DataResult<TransportVechile>.Fail(existValidationResult);

            DataResult<TransportVehiclesTypes> typeValidationResult = ValidateType(transportVechileBlank);
            if (typeValidationResult.IsFail(out TransportVehiclesTypes type)) return DataResult<TransportVechile>.Fail(typeValidationResult);

            DataResult<String> nameValidationResult = await ValidateName(transportVechileBlank);
            if (nameValidationResult.IsFail(out String name)) return DataResult<TransportVechile>.Fail(nameValidationResult);

            DataResult<String> stateNumberValidationResult = ValidateStateNumber(transportVechileBlank);
            if (stateNumberValidationResult.IsFail(out String stateNumber)) return DataResult<TransportVechile>.Fail(stateNumberValidationResult);

            DataResult<Double> averageSpeedValidationResult = ValidateAverageSpeed(transportVechileBlank);
            if (averageSpeedValidationResult.IsFail(out Double averageSpeed)) return DataResult<TransportVechile>.Fail(averageSpeedValidationResult);

            DataResult<Double> fuelConsumptionValidationResult = ValidateFuelConsumption(transportVechileBlank);
            if (fuelConsumptionValidationResult.IsFail(out Double fuelConsumption)) return DataResult<TransportVechile>.Fail(fuelConsumptionValidationResult);

            TransportVechile transportVechile = new(
                id ?? Guid.NewGuid(),
                type,
                name,
                stateNumber,
                averageSpeed,
                fuelConsumption,
                isRemoved: false
            );

            return DataResult<TransportVechile>.Success(transportVechile);
        }

        private async Task<DataResult<Guid?>> ValidateExistTransportVechile(TransportVechileBlank transportVechileBlank)
        {
            if (transportVechileBlank.Id is not { } id)
                return DataResult<Guid?>.Success(null);

            TransportVechile existTransportVechile = await GetTransportVechile(id);
            if (existTransportVechile.IsRemoved) return DataResult<Guid?>.Fail("Транспорт удален");

            return DataResult<Guid?>.Success(id);
        }

        private DataResult<TransportVehiclesTypes> ValidateType(TransportVechileBlank transportVechileBlank)
        {
            if (transportVechileBlank.Type is not { } type)
                return DataResult<TransportVehiclesTypes>.Fail("Выберите тип транспортного средства");

            if (!Enum.IsDefined(type))
                throw new Exception($"Тип {type} не существует");

            return DataResult<TransportVehiclesTypes>.Success(type);
        }

        private async Task<DataResult<String>> ValidateName(TransportVechileBlank transportVechileBlank)
        {
            if (String.IsNullOrWhiteSpace(transportVechileBlank.Name))
                return DataResult<String>.Fail("Не указано название транспортного средства");

            const Int32 maxTransportVechileNameLength = 255;
            if (transportVechileBlank.Name.Length >= maxTransportVechileNameLength)
                return DataResult<String>.Fail($"Название транспортного средства слишком длинное. Максимально допустимо {maxTransportVechileNameLength} символов");

            TransportVechile? transportVechileWithSameName = await GetTransportVechile(transportVechileBlank.Name);
            if (transportVechileWithSameName is not null && transportVechileWithSameName.Id != transportVechileBlank.Id)
                return DataResult<String>.Fail("Транспортного средства с таким названием уже существует");

            return DataResult<String>.Success(transportVechileBlank.Name);
        }

        private DataResult<String> ValidateStateNumber(TransportVechileBlank transportVechileBlank)
        {
            Regex regex = new Regex(@"[АВЕКМНОРСТУХ]\d{3}[АВЕКМНОРСТУХ]{2}\d{2,3}");
            if (String.IsNullOrWhiteSpace(transportVechileBlank.StateNumber))
                return DataResult<String>.Fail("Не указан государственный номер");
            if(regex.IsMatch(transportVechileBlank.StateNumber))
                return DataResult<String>.Fail("Государственный номер не соответствует российскому стандарту");
            return DataResult<String>.Success(transportVechileBlank.StateNumber);
        }
        private DataResult<Double> ValidateAverageSpeed(TransportVechileBlank transportVechileBlank)
        {
            if (transportVechileBlank.AverageSpeed is not { } averageSpeed)
                return DataResult<Double>.Fail("Не указана средняя скорость");

            if (averageSpeed <= 0)
                return DataResult<Double>.Fail("Средняя скорость должна быть больше нуля");

            return DataResult<Double>.Success(averageSpeed);
        }
        private DataResult<Double> ValidateFuelConsumption(TransportVechileBlank transportVechileBlank)
        {
            const Int32 maxTransportVechileFuelConsumption = 117;
            if (transportVechileBlank.FuelConsumption is not { } fuelConsumption)
                return DataResult<Double>.Fail("Не указан разход топлива");
            if (fuelConsumption <= 0)
                return DataResult<Double>.Fail("Разход топлива должен быть больше нуля");
            if(fuelConsumption > maxTransportVechileFuelConsumption)
                return DataResult<Double>.Fail("Разход топлива не может быть таким большим");
            return DataResult<Double>.Success(fuelConsumption);
        }
        #endregion Validation

        public async Task<TransportVechile> GetTransportVechile(Guid transportVechileId)
        {
            TransportVechile? transportVechile = await transportVechilesRepository.GetTransportVechile(transportVechileId);
            if (transportVechile is null) throw new Exception($"Транспорт {transportVechileId} не найден");

            return transportVechile;
        }

        private Task<TransportVechile?> GetTransportVechile(String name)
        {
            return transportVechilesRepository.GetTransportVechile(name);
        }

        public Task<Page<TransportVechile>> GetTransportVechiles(Int32 page, Int32 countInPage)
        {
            return transportVechilesRepository.GetTransportVechiles(page, countInPage);
        }

        public async Task<Result> RemoveTransportVechile(Guid id)
        {
            TransportVechile transportVechile = await GetTransportVechile(id);
            if (transportVechile.IsRemoved) return Result.Fail("Транспорт уже удален");

            await transportVechilesRepository.RemoveTransportVechile(id);

            return Result.Success();
        }
    }

}

