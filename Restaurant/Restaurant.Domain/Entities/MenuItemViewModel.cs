using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;


namespace Restaurant.Domain.Entities
{
    public class MenuItemViewModel : BaseEntity
    {
        public MenuItem MenuItem { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> FoodTypeList { get; set; }
    }
}
