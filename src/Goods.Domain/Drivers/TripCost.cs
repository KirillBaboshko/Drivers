using Goods.Domain.Drivers.Enums;
using Goods.Domain.TransportVehicles;
using Goods.Tools.Types.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Goods.Domain.Drivers
{
    public class TripCost(Driver driver, DateOnly day)
    {

        public static Double CalculateDriverPayment(Driver driver, DateOnly day)
        {
            DateOnly[] Weekends = new[] { new DateOnly(2026, 3, 8), new DateOnly(2026, 1, 1) };
            const Double WeekendExtraСharge = 2;
            const Double DriverPaymentExtraСharge = 0.01;

            if (day.DayOfWeek == DayOfWeek.Sunday || day.DayOfWeek == DayOfWeek.Saturday || Weekends.Contains(day))
            {
                return (((driver.Experience() * DriverPaymentExtraСharge) * driver.Payment) + driver.Payment)* WeekendExtraСharge;
            }
            return ((driver.Experience() * DriverPaymentExtraСharge) * driver.Payment) + driver.Payment;
        }
        public static Double PriceWithExtraСharge(Driver driver, DateOnly day) 
        {
            const Double ExtraСharge = 0.3;
            const Int32 LengthOfWay = 100;
            const Double PriceOfFuel = 70;
            Double timeSpent = (LengthOfWay / driver.TransportVechile.AverageSpeed);
            Double fuelSpent = driver.TransportVechile.FuelConsumption * PriceOfFuel* timeSpent;
            Double priceWithoutExtraСharge= fuelSpent + timeSpent * CalculateDriverPayment(driver, day);
            priceWithoutExtraСharge += priceWithoutExtraСharge * ExtraСharge;
            return priceWithoutExtraСharge;
        }
        public Double ResultPrice { get; } = PriceWithExtraСharge(driver, day);

        public static TripCost Calculate(Driver driver, DateOnly day)
        {
            return new TripCost(driver,day);
        }
    }
  }

