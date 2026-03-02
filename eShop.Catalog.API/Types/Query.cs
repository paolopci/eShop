using System;
using eShop.Catalog.API.Data;
using eShop.Catalog.API.Models;
using Microsoft.EntityFrameworkCore;

namespace eShop.Catalog.API.Types;

public class Query()
{

    public IQueryable<Product> GetProducts(CatalogContext context)
    {
        return context.Products;
    }

    public async Task<Product?> GetProductById(int id, CatalogContext context)
            => await context.Products.FirstOrDefaultAsync(p => p.Id == id);
}
