using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goods.Domain.Drivers.Enums
{
    public enum RightsCategory
    {
        [Display(Name = "Мопеды и легкие квадрициклы (M)")]
        CategoryM = 1,

        [Display(Name = "Мотоциклы (A)")]
        CategoryA = 2,

        [Display(Name = "Легковые автомобили (B)")]
        CategoryB = 3,

        [Display(Name = "Грузовые автомобили (C)")]
        CategoryC = 4,

        [Display(Name = "Автобусы (D)")]
        CategoryD = 5
    }
}
