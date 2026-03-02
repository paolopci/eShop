using System;
using eShop.Catalog.API.Data;
using eShop.Catalog.API.Models;
using Microsoft.EntityFrameworkCore;

namespace eShop.Catalog.API.Types;

public class Query()
{

    // se lascio questi setting in UsePaging questi sovrascrivono quelli che ho impostato in program.cs
    // con l'uso .ModifyPagingOptions(options => ....
    [UsePaging(DefaultPageSize = 1, MaxPageSize = 11)]
    [UseProjection] // con questo attributo ho  inserito un middleware nella mia pipeline
    public IQueryable<Product> GetProducts(CatalogContext context)
    {
        return context.Products;
    }

    [UseFirstOrDefault]// con questo attributo ho  inserito un middleware nella mia pipeline
    [UseProjection]// con questo attributo ho  inserito un middleware nella mia pipeline
    // importante è l'ordinecon il quale li inseriamo si parte dall'alto verso il basso
    public IQueryable<Product> GetProductById(int id, CatalogContext context)
                    => context.Products.Where(p => p.Id == id);
}
