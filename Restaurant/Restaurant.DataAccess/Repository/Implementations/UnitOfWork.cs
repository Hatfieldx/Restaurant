using Restaurant.DataAccess.Repository.Interfaces;
using System;

namespace Restaurant.DataAccess.Repository.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RestaurantContext _context;

        private bool _disposed;
        public UnitOfWork(RestaurantContext context)
        {
            _context = context;

            Category = new CategoryRepository(_context);

            FoodType = new FoodTypeRepository(_context);
        }

        public ICategoryRepository Category { get; private set; }
        public IFoodTypeRepository FoodType { get; private set; }

        public void Dispose()
        {
            _context.Dispose();
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                Dispose();
                GC.SuppressFinalize(this);
            }
            _disposed = true;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        ~UnitOfWork() 
        {
            Dispose(false);
        }
    }
}
