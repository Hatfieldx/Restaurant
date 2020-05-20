using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Repository.Interfaces;
using Restaurant.Domain.Entities;

namespace Restaurant.Mvc
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitofwork;
        public UpsertModel(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }
        [BindProperty]
        public Category CategoryObj { set; get; }
        
        public IActionResult OnGet(int? id)
        {
            CategoryObj = new Category();

            if (id.HasValue)
            {
                CategoryObj = _unitofwork.Category.GetFirstOrDefault(x => x.Id == id);

                if (CategoryObj == null)
                {
                    return NotFound();
                }
            }
            return Page();
        }
         
        public IActionResult OnPost()
        {     
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (CategoryObj.Id == 0)
            {
                _unitofwork.Category.Add(CategoryObj);
            }
            else
            {
                _unitofwork.Category.Update(CategoryObj);
            }
            _unitofwork.Save();

            return RedirectToPage("./Index");
        }
    }
}