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
        DateOnly birthDate, DateOnly receivingDate, TransportVechile transportVechile,Double payment, Boolean isRemoved)
    {
        public Guid Id { get; } = id;
        public String Name { get; } = name;
        public String Surname { get; }= surname;
        public String? Patronymic { get; }= patronymic;
        public Gender Gender { get; }= gender;
        public RightsCategory[] RightsCategories { get; }= rightsCategories;
        public DateOnly BirthDate { get; } = birthDate;
        public Int32 Age()=> DateTime.Now.DayOfYear-BirthDate.DayOfYear>0? DateTime.Now.Year- BirthDate.Year : DateTime.Now.Year - BirthDate.Year-1;
        public DateOnly ReceivingDate { get; } = receivingDate;
        public Int32 Experience() => DateTime.Now.DayOfYear - ReceivingDate.DayOfYear > 0 ? DateTime.Now.Year - ReceivingDate.Year : DateTime.Now.Year - ReceivingDate.Year - 1;
        public TransportVechile TransportVechile { get; }= transportVechile;
        public Double Payment { get; }= payment;
        public Boolean IsRemoved { get; }= isRemoved;
    }

}
