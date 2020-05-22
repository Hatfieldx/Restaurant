using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Repository.Interfaces;
using Restaurant.Domain.Entities;

namespace Restaurant.Mvc.FoodType
{
    public class UpsertModel : PageModel
    {
        [BindProperty]
        public Restaurant.Domain.Entities.FoodType FoodTypeObj { get; set; }

        private readonly IUnitOfWork _unitOfWork;
        public UpsertModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;            
        }
        
        public IActionResult OnGet(int? id)
        {
            FoodTypeObj = new Domain.Entities.FoodType();

            if (id.HasValue)
            {
                FoodTypeObj = _unitOfWork.FoodType.GetFirstOrDefault(x => x.Id == id);

                if (FoodTypeObj == null)
                {
                    return NotFound();
                }
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (FoodTypeObj.Id == 0)
            {
                _unitOfWork.FoodType.Add(FoodTypeObj);
            }
            else
            {
                var item = _unitOfWork.FoodType.GetFirstOrDefault(x => x.Id == FoodTypeObj.Id);

                if (item != null)
                {
                    _unitOfWork.FoodType.Update(FoodTypeObj);
                }
            }

            _unitOfWork.Save();

            return RedirectToPage("./Index");
        }
    }
}