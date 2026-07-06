using Goods.Domain.TransportVehicles.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Goods.Domain.TransportVehicles
{
    public class TransportVechileBlank
    {
        public Guid? Id { get; set; }
        public TransportVehiclesTypes? Type { get; set; } 
        public String? Name { get; set; }
        public String? StateNumber { get; set; } 
        public Double? AverageSpeed { get; set; } 
        public Double? FuelConsumption { get; set; } 
    }
}
