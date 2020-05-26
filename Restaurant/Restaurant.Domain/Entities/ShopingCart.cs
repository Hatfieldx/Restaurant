using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Restaurant.Domain.Entities
{
    public class ShopingCart
    {
        public int Id { get; set; }

        public int MenuItemId { get; set; }

        [ForeignKey("MenuItemId")]
        public int MyProperty { get; set; }
        public string ApplicationUserId { get; set; }
        [Range(1, 100)]
        public int Count { get; set; } = 1;
    }
}
