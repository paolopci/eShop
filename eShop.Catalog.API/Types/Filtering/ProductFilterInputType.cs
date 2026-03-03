using System;
using eShop.Catalog.API.Models;
using HotChocolate.Data.Filters;

namespace eShop.Catalog.API.Types.Filtering
{
    public sealed class ProductFilterInputType : FilterInputType<Product>
    {
        protected override void Configure(IFilterInputTypeDescriptor<Product> descriptor)
        {
            descriptor.BindFieldsExplicitly();

            descriptor.Field(t => t.Name).Type<SearchStringOperationFilterInputType>();
            descriptor.Field(t => t.Type);
            descriptor.Field(t => t.Brand);
            descriptor.Field(t => t.Price);
            descriptor.Field(t => t.AvailableStock);
        }
    }
}