using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Restaurant.Domain.Entities
{
    public class FoodType : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
