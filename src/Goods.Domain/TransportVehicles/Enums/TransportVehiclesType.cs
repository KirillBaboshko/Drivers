using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goods.Domain.TransportVehicles.Enums
{
    public enum TransportVehiclesTypes
    {
        [Display(Name = "Легковой автомобиль")]
        PassengerCar= 1,

        [Display(Name = "Грузовик")]
        Truck = 2,

        [Display(Name = "Автобус")]
        Bus = 3
    }
}
