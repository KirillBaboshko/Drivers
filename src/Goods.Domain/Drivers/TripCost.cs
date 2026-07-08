using Goods.Domain.Drivers.Enums;
using Goods.Domain.TransportVehicles;
using Goods.Tools.Types.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goods.Domain.Drivers
{
        public class TripCost(Int32 lengthOfWay, Double priceFuel,Double pricePayment, Double timeSpent,Double fuelSpent,Double extraCharge)
        {
            public Int32 LengthOfWay { get; }= lengthOfWay;
            public Double PriceOfFuel { get; } = priceFuel;
            public Double PriceOfPayment { get; } = pricePayment;
            public Double TimeSpent { get; }= timeSpent;
            public Double FuelSpent { get; } = fuelSpent;
            public Double ExtraCharge { get; } = extraCharge;
        }
}
