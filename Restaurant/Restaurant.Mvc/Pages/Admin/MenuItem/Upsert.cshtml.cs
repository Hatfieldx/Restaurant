using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Repository.Interfaces;
using Restaurant.Domain.Entities;

namespace Restaurant.Mvc.MenuItem
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitofwork;

        private readonly IWebHostEnvironment _env;
        public UpsertModel(IUnitOfWork unitofwork, IWebHostEnvironment env)
        {
            _unitofwork = unitofwork;
            _env = env;
        }
        [BindProperty]
        public MenuItemViewModel MenuItemObj { set; get; }
        
        public IActionResult OnGet(int? id)
        {
            MenuItemObj = new MenuItemViewModel
            {
                MenuItem = new Domain.Entities.MenuItem(),

                CategoryList = _unitofwork.Category.GetCategoryListForDropDown(),

                FoodTypeList = _unitofwork.FoodType.GetFoodTypeListForDropDown()
            };

            if (id.HasValue)
            {
                MenuItemObj.MenuItem = _unitofwork.MenuItem.GetFirstOrDefault(x => x.Id == id);

                if (MenuItemObj.MenuItem == null)
                {
                    return NotFound();
                }
            }
            return Page();
        }
         
        public IActionResult OnPost()
        {
            string webRootPath = _env.WebRootPath;

            var files = HttpContext.Request.Form.Files;            
            
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (MenuItemObj.MenuItem.Id == 0)
            {
                IFormFile file = files.FirstOrDefault();

                if (file == null)
                {
                    return Page();
                }
                
                string filename = Guid.NewGuid().ToString();

                var uploads = Path.Combine(webRootPath, "images", "menuitem");

                var ext = Path.GetExtension(file.FileName);

                using (var fs = new FileStream(Path.Combine(uploads, filename + ext), FileMode.Create))
                {
                    file.CopyTo(fs);
                }

                MenuItemObj.MenuItem.Image = Path.Combine("images", "menuitem", filename + ext);
                
                _unitofwork.MenuItem.Add(MenuItemObj.MenuItem);
            }
            else
            {
                var obj = _unitofwork.MenuItem.Get(MenuItemObj.MenuItem.Id);

                if (files.Count > 0)
                {
                    string filename = Guid.NewGuid().ToString();

                    var uploads = Path.Combine(webRootPath, "images", "menuitem");

                    var ext = Path.GetExtension(files[0].FileName);

                    var currentImage = Path.Combine(_env.WebRootPath, obj.Image);

                    if (System.IO.File.Exists(currentImage))
                    {
                        System.IO.File.Delete(currentImage);
                    }                    
                    
                    using (var fs = new FileStream(Path.Combine(uploads, filename + ext), FileMode.Create))
                    {
                        files[0].CopyTo(fs);
                    }

                    MenuItemObj.MenuItem.Image = Path.Combine("images", "menuitem", filename + ext);                    
                }
                else
                {
                    MenuItemObj.MenuItem.Image = obj.Image;
                }

                _unitofwork.MenuItem.Update(MenuItemObj.MenuItem);

            }
            _unitofwork.Save();

            return RedirectToPage("./Index");
        }
    }
}