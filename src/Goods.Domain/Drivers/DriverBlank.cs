using Goods.Domain.Drivers.Enums;
using Goods.Domain.TransportVehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Goods.Domain.Drivers
{
    public class DriverBlank
    {
        public Guid? Id { get; set; } 
        public String? Name { get; set; } 
        public String? Surname { get; set; } 
        public String? Patronymic { get; set; }
        public Gender? Gender { get; set; } 
        public RightsCategory[]? RightsCategories { get; set; }
        public Int32? Age { get; set; } 
        public Int32? Experience { get; set; }
        public Guid? TransportVechileId { get; set; }
        public Double? Payment { get; set; }
    }
}
