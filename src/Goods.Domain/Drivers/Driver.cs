using Goods.Domain.Drivers.Enums;
using Goods.Domain.TransportVehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goods.Domain.Drivers
{
    public class Driver(Guid id,String name, String surname, String? patronymic, Gender gender, RightsCategory[] rightsCategories, 
        Int32 age,Int32 experience, TransportVechile transportVechile,Double payment, Boolean isRemoved)
    {
        public Guid Id { get; } = id;
        public String Name { get; } = name;
        public String Surname { get; }= surname;
        public String? Patronymic { get; }= patronymic;
        public Gender Gender { get; }= gender;
        public RightsCategory[] RightsCategories { get; }= rightsCategories;
        public Int32 Age { get; }= age;
        public Int32 Experience { get; } = experience;
        public TransportVechile TransportVechile { get; }= transportVechile;
        public Double Payment { get; }= payment;
        public Boolean IsRemoved { get; }= isRemoved;
    }

}
