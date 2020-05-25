using Microsoft.EntityFrameworkCore;
using Restaurant.DataAccess.Repository.Interfaces;
using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Restaurant.DataAccess.Repository.Implementations
{
    public class FoodTypeRepository : Repository<FoodType>, IFoodTypeRepository
    {
        private readonly DbSet<FoodType> _dbSet;
        
        public FoodTypeRepository(RestaurantContext context)
            : base(context) {
            
            _dbSet = context.FoodTypes;

        }
        public IEnumerable<SelectListItem> GetFoodTypeListForDropDown()
        {
            return _dbSet.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()

            });            
        }

        public void Update(FoodType foodtype)
        {
            var finded = _dbSet.FirstOrDefault(x => x.Id == foodtype.Id);

            if (finded == null)
            {
                throw new InvalidOperationException($"not found food type by id {foodtype.Id}");
            }

            finded.Name = foodtype.Name;

            _dbSet.Update(finded);

            _context.SaveChanges();
        }
    }
}
