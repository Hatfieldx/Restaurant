using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Repository.Interfaces;

namespace Restaurant.Mvc.Pages.Customer.Home
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IEnumerable<Domain.Entities.MenuItem> MenuItemList { get; set; }
        public IEnumerable<Domain.Entities.Category> CategoryList { get; set; }

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult OnGet()
        {
            MenuItemList = _unitOfWork.MenuItem.GetAll(null, null, "Category,FoodType");
            CategoryList = _unitOfWork.Category.GetAll(null, x => x.OrderBy(c => c.DisplayOrder));

            return Page();
        }


    }
}