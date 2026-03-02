using System;
using eShop.Catalog.API.Data;
using eShop.Catalog.API.Models;
using Microsoft.EntityFrameworkCore;

namespace eShop.Catalog.API.Types;

public class Query()
{

    [UseProjection]
    public IQueryable<Product> GetProducts(CatalogContext context)
    {
        return context.Products;
    }

    [UseFirstOrDefault]
    [UseProjection]
    public IQueryable<Product> GetProductById(int id, CatalogContext context)
                    => context.Products.Where(p => p.Id == id);
}
