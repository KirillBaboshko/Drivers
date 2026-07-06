using Goods.Domain.TransportVehicles.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goods.Domain.TransportVehicles
{
    public class TransportVechile(Guid id, TransportVehiclesTypes type, String name,String stateNumber,Double averageSpeed,Double fuelConsumption, Boolean isRemoved)
    {
        public Guid Id { get; } = id;
        public TransportVehiclesTypes Type { get; } = type;
        public String Name { get; } = name;
        public String StateNumber { get; }= stateNumber;
        public Double AverageSpeed { get; } = averageSpeed;
        public Double FuelConsumption { get; } = fuelConsumption;
        public Boolean IsRemoved { get; }= isRemoved;
    }
}
