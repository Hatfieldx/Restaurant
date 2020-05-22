using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.DataAccess.Repository.Interfaces;

namespace Restaurant.Mvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public FoodTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = _unitOfWork.FoodType.GetAll() });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _unitOfWork.FoodType.GetFirstOrDefault(x => x.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            _unitOfWork.FoodType.Remove(item);

            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete succesful" });        
        }

    }
}