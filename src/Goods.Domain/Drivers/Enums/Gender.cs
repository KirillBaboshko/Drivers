using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goods.Domain.Drivers.Enums
{ 
     public enum Gender
    {
        [Display(Name = "Мужской")]
        Male = 1,

        [Display(Name = "Женский")]
        Female = 2
    }
}
