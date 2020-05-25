using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.DataAccess.Repository.Interfaces;
using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Restaurant.DataAccess.Repository.Implementations
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {        
        public CategoryRepository(RestaurantContext context)
            : base(context)
        {            
        }
        public IEnumerable<SelectListItem> GetCategoryListForDropDown()
        {
            return _context.Categories.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
        }

        public void Update(Category category)
        {
            var entity = _context.Categories.FirstOrDefault(s => s.Id == category.Id);

            if (entity == null)
            {
                throw new InvalidOperationException($"not found category by id {category.Id}");
            }

            entity.Name = category.Name;

            entity.DisplayOrder = category.DisplayOrder;

            entity.Icon = category.Icon;

            _context.SaveChanges();
        }
    }
}
