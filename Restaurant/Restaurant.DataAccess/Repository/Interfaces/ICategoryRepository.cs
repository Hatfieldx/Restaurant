using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Restaurant.DataAccess.Repository.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IEnumerable<SelectListItem> GetCategoryListForDropDown();
        void Update(Category category);
    }
}
