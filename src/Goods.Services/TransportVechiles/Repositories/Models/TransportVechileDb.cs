using Goods.Domain.Drivers.Enums;
using Goods.Domain.TransportVehicles.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goods.Services.TransportVehicles.Repositories.Models
{
    public class TransportVechileDb
    {
        public Guid Id { get; set; }
        public TransportVehiclesTypes Type { get; }
        public String Name { get; set; }
        public String StateNumber { get; set; }
        public Double AverageSpeed { get; set; }
        public Double FuelConsumption { get; set; }
        public DateTime CreatedDateTimeUtc { get; set; }
        public DateTime? ModifiedDateTimeUtc { get; set; }
        public Boolean IsRemoved { get; set; }

        public TransportVechileDb(
          Guid id, 
          TransportVehiclesTypes type,
          String name,
          String stateNumber,
          Double averageSpeed,
          Double fuelConsumption,
          DateTime createdDateTimeUtc,
          DateTime? modifiedDateTimeUtc,
          Boolean isRemoved
       )
        {
            Id = id;
            Type = type;
            Name = name;
            StateNumber = stateNumber;
            AverageSpeed= averageSpeed;
            FuelConsumption = fuelConsumption;
            CreatedDateTimeUtc = createdDateTimeUtc;
            ModifiedDateTimeUtc = modifiedDateTimeUtc;
            IsRemoved= isRemoved;
            CreatedDateTimeUtc = createdDateTimeUtc;
            ModifiedDateTimeUtc = modifiedDateTimeUtc;
            IsRemoved = isRemoved;
        }
    }
}
