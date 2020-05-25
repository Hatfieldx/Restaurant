using Microsoft.EntityFrameworkCore;
using Restaurant.DataAccess.Repository.Interfaces;
using Restaurant.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Restaurant.DataAccess.Repository.Implementations
{
    public class MenuItemRepository : Repository<MenuItem>, IMenuItemRepository
    {
        public MenuItemRepository(RestaurantContext dbcontext)
            : base(dbcontext)
        {
        }

        public IEnumerable<MenuItem> GetMenuItems()
        {            
            return _context.MenuItems
                .AsNoTracking()
                .Include(x => x.FoodType)
                .Include(x => x.Category)
                .ToArray();                
        }

        public void Update(MenuItem menuitem)
        {
            var item = _context.MenuItems.FirstOrDefault(x => x.Id == menuitem.Id);

            if (item == null)
            {
                throw new InvalidOperationException($"menu item with id {menuitem.Id} was not found");
            }

            item.Image = menuitem.Image;
            item.Name = menuitem.Name;
            item.Price = menuitem.Price;
            item.CategoryId = menuitem.CategoryId;
            item.FoodTypeId = menuitem.FoodTypeId;
            item.Description = menuitem.Description;

            _context.MenuItems.Update(item);
        }
    }
}
