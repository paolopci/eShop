using System;
using eShop.Catalog.API.Data;
using eShop.Catalog.API.Models;
using HotChocolate.Data.Filters;
using HotChocolate.Data.Sorting;
using Microsoft.EntityFrameworkCore;

namespace eShop.Catalog.API.Types;

public class Query()
{
    [UsePaging(DefaultPageSize = 1, MaxPageSize = 10)]
    [UseProjection]
    // abilita il filtro GraphQL (argomento where) sui campi di Brand esposti dalla query
    [UseFiltering]
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
    // abilita il filtro GraphQL (argomento where) sui campi di Product esposti dalla query
    [UseFiltering<ProductFilterInputType>] // rende disponibile la definizione di filtro personalizzata che hai scritto (campi e operazioni consentite);
    [UseSorting]
    public IQueryable<Product> GetProducts(CatalogContext context, IFilterContext filterContext, ISortingContext sortingContext)
    {
        filterContext.Handled(false);
        sortingContext.Handled(false);

        IQueryable<Product> query = context.Products;

        /*
        Quindi: serve a definire un comportamento “di default” quando where e order non sono presenti nella richiesta GraphQL.
        */

        if (!filterContext.IsDefined)
        {
            query = query.Where(t => t.BrandId == 1);
        }
        if (!sortingContext.IsDefined)
        {
            query = query.OrderBy(t => t.Brand!.Name).ThenByDescending(t => t.Price);
        }

        return query;
    }

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
    // abilita il filtro GraphQL (argomento where) sui campi di ProductType esposti dalla query
    [UseFiltering]
    public IQueryable<ProductType> GetProductTypes(CatalogContext context)
        => context.ProductTypes;

    [UseFirstOrDefault]
    [UseProjection]
    public IQueryable<ProductType> GetProductTypeById(int id, CatalogContext context)
        => context.ProductTypes.Where(t => t.Id == id);
}


public class ProductFilterInputType : FilterInputType<Product>
{
    protected override void Configure(IFilterInputTypeDescriptor<Product> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Field(t => t.Name);
        descriptor.Field(t => t.Type);
        descriptor.Field(t => t.Brand);
        descriptor.Field(t => t.Price);
        descriptor.Field(t => t.AvailableStock);
    }
}

