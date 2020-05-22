using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace Restaurant.DataAccess.Repository.Interfaces
{
    public interface IFoodTypeRepository : IRepository<FoodType>
    {
        IEnumerable<SelectListItem> GetFoodTypeListForDropDown();
        void Update(FoodType foodtype);
    }
}
