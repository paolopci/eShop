using eShop.Catalog.API.Models;
using HotChocolate.Data.Sorting;

namespace eShop.Catalog.API.Types.Sorting;

public sealed class BrandSortInputType : SortInputType<Brand>
{
    protected override void Configure(ISortInputTypeDescriptor<Brand> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Field(t => t.Name);
    }
}
