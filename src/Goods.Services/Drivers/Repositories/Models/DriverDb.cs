using Goods.Domain.Drivers.Enums;
using Goods.Domain.Products;
using Goods.Domain.TransportVehicles;
using Goods.Services.TransportVehicles.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goods.Services.Drivers.Repositories.Models
{
    public class DriverDb
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }
        public String? Patronymic { get; set; }
        public Gender Gender { get; set; }
        public RightsCategory[] RightsCategories { get; set; }
        public Int32 Age { get; set; }
        public Int32 Experience { get; set; }
        public TransportVechileDb TransportVechileDb { get; set; }
        public Double Payment { get; set; }
        public DateTime CreatedDateTimeUtc { get; set; }
        public DateTime? ModifiedDateTimeUtc { get; set; }
        public Boolean IsRemoved { get; set; }

        public DriverDb(
           Guid id,
          String name, 
          String surname, 
          String? patronymic, 
          Gender gender, 
          RightsCategory[] rightsCategories,
          Int32 age, 
          Int32 experience,
          TransportVechileDb transportVechileDb, 
          Double payment,
          DateTime createdDateTimeUtc,
          DateTime? modifiedDateTimeUtc,
          Boolean isRemoved
       )
        {
            Id = id;
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            Gender = gender;
            RightsCategories = rightsCategories;
            Age=age;
            Experience = experience;
            TransportVechileDb = transportVechileDb;
            Payment=payment;
            CreatedDateTimeUtc = createdDateTimeUtc;
            ModifiedDateTimeUtc = modifiedDateTimeUtc;
            IsRemoved = isRemoved;
        }
    }
}
