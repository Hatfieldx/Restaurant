using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.DataAccess.Repository.Implementations;
using Restaurant.DataAccess.Repository.Interfaces;

namespace Restaurant.Mvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;

        public MenuItemController(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _env = env;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = ((MenuItemRepository)_unitOfWork.MenuItem).GetMenuItems() });
        }
        

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var objFromDb = _unitOfWork.MenuItem.GetFirstOrDefault(u => u.Id == id);
                if (objFromDb == null)
                {
                    return Json(new { success = false, message = "Error while deleting." });
                }

                var imagePath = Path.Combine(_env.WebRootPath, objFromDb.Image.TrimStart('\\'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

                _unitOfWork.MenuItem.Remove(objFromDb);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error while deleting." });
            }
            return Json(new { success = true, message = "Delete success." });
        }

    }
}