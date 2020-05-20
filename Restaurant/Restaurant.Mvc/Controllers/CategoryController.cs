using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Restaurant.DataAccess.Repository.Interfaces;

namespace Restaurant.Mvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitofwork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitofwork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = _unitofwork.Category.GetAll() });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {        
            var item = _unitofwork.Category.GetFirstOrDefault(x => x.Id == id);

            if (item == null)
            {
                return Json(new {success = false, message = "Error while deleting" });
            }

            _unitofwork.Category.Remove(item);

            _unitofwork.Save();

            return Json(new { success = true, message = "Delete succesful" });
        }
    }
}