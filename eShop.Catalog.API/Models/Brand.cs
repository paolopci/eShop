using System;
using System.ComponentModel.DataAnnotations;
using eShop.Catalog.API.Types.Configuration;

namespace eShop.Catalog.API.Models
{
    public class Brand
    {
        public int Id { get; set; }

        [Required]
        [UseToUpper]
        public string Name { get; set; } = default!;

        public ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
