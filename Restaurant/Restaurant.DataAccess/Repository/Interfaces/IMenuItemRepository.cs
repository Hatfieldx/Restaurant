
using Restaurant.Domain.Entities;

namespace Restaurant.DataAccess.Repository.Interfaces
{
    public interface IMenuItemRepository : IRepository<MenuItem>
    {
        void Update(MenuItem menuitem);
    }
}
