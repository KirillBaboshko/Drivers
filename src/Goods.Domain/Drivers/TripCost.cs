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
    public class TripCost(Driver driver)
    {
        private const Int32 LengthOfWay = 100;
        private const Double PriceOfFuel = 70;
        private const Double DriverPaymentExtraСharge = 0.01;
        private const Double ExtraСharge = 0.3;
        public static Double CalculateDriverPayment(Driver driver)
        {
            return ((driver.Experience() * DriverPaymentExtraСharge) * driver.Payment) + driver.Payment;
        }
        public static Double PriceWithoutExtraСharge(Driver driver) => (LengthOfWay / driver.TransportVechile.AverageSpeed) * driver.TransportVechile.FuelConsumption * PriceOfFuel +
            (LengthOfWay / driver.TransportVechile.AverageSpeed) * CalculateDriverPayment(driver);
        public Double ResultPrice { get; } = PriceWithoutExtraСharge(driver) * ExtraСharge + PriceWithoutExtraСharge(driver);

        public static TripCost Calculate(Driver driver)
        {
            return new TripCost(driver);
        }
    }
  }

