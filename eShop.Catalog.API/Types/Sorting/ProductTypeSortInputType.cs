using eShop.Catalog.API.Models;
using HotChocolate.Data.Sorting;

namespace eShop.Catalog.API.Types.Sorting;

public sealed class ProductTypeSortInputType : SortInputType<ProductType>
{
    protected override void Configure(ISortInputTypeDescriptor<ProductType> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Field(t => t.Name);
    }
}