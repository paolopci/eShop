using System;
using System.ComponentModel.DataAnnotations;

namespace eShop.Catalog.API.Models;

public class ProductType
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = default!;
    public ICollection<Product> Products { get; } = new List<Product>();
}
