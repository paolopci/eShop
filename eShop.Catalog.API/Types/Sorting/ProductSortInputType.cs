using System;
using eShop.Catalog.API.Models;
using HotChocolate.Data.Sorting;

namespace eShop.Catalog.API.Types.Sorting;

public sealed class ProductSortInputType : SortInputType<Product>
{
    protected override void Configure(ISortInputTypeDescriptor<Product> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Field(t => t.Name);
        descriptor.Field(t => t.Type).Type<ProductTypeSortInputType>();
        descriptor.Field(t => t.Brand).Type<BrandSortInputType>();
        descriptor.Field(t => t.Price);
    }
}
