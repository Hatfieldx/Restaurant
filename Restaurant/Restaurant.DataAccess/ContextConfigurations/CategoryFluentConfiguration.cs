using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities;
using System;

namespace Restaurant.DataAccess.ContextConfigurations
{
    public class CategoryFluentConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Category> builder)
        {
            throw new NotImplementedException();
        }
    }
}
