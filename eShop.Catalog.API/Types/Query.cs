using System;
using eShop.Catalog.API.Data;
using eShop.Catalog.API.Models;
using Microsoft.EntityFrameworkCore;

namespace eShop.Catalog.API.Types;

public class Query()
{
    [UsePaging(DefaultPageSize = 1, MaxPageSize = 10)]
    [UseProjection]
    public IQueryable<Brand> GetBrands(CatalogContext context)
        => context.Brands;

    [UseFirstOrDefault]
    [UseProjection]
    public IQueryable<Brand> GetBrandById(int id, CatalogContext context)
        => context.Brands.Where(t => t.Id == id);

    // se lascio questi setting in UsePaging questi sovrascrivono quelli che ho impostato in program.cs
    // con l'uso .ModifyPagingOptions(options => ....
    [UsePaging(DefaultPageSize = 1, MaxPageSize = 11)]
    [UseProjection] // con questo attributo ho  inserito un middleware nella mia pipeline
    public IQueryable<Product> GetProducts(CatalogContext context)
        => context.Products;

    [UseFirstOrDefault]// con questo attributo ho  inserito un middleware nella mia pipeline
    [UseProjection]// con questo attributo ho  inserito un middleware nella mia pipeline
    // importante: è l'ordine con il quale li inseriamo, si parte dall'alto verso il basso
    public IQueryable<Product> GetProductById(int id, CatalogContext context)
        => context.Products.Where(p => p.Id == id);

    [UsePaging]
    [UseProjection]
    public IQueryable<Product> GetProductsByType(int typeId, CatalogContext context)
        => context.Products
            .AsNoTracking()
            .Where(p => p.TypeId == typeId);

    [UsePaging]
    [UseProjection]
    public IQueryable<Product> GetProductsByBrand(int brandId, CatalogContext context)
        => context.Products
            .AsNoTracking()
            .Where(p => p.BrandId == brandId);

    [UsePaging]
    [UseProjection]
    public IQueryable<ProductType> GetProductTypes(CatalogContext context)
        => context.ProductTypes;

    [UseFirstOrDefault]
    [UseProjection]
    public IQueryable<ProductType> GetProductTypeById(int id, CatalogContext context)
        => context.ProductTypes.Where(t => t.Id == id);
}
