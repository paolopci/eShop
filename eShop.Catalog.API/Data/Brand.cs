using System;
using System.ComponentModel.DataAnnotations;

namespace eShop.Catalog.API.Data;

public class Brand
{
    [Required]
    public string Name { get; set; } = default!;

    public ICollection<Product> Products { get; set; } = new List<Product>();

}
