using System;

namespace Restaurant.DataAccess.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Category { get; }
        void Save();
    }
}
