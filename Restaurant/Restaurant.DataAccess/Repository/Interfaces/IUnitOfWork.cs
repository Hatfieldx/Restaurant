using System;

namespace Restaurant.DataAccess.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Category { get; }
        IFoodTypeRepository FoodType { get; }
        IMenuItemRepository MenuItem { get; }
        void Save();
    }
}
