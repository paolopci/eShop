using System;
using HotChocolate.Data.Filters;

namespace eShop.Catalog.API.Types.Filtering;

public sealed class SearchStringOperationFilterInputType : StringOperationFilterInputType
{
    protected override void Configure(IFilterInputTypeDescriptor descriptor)
    {
        descriptor.Operation(DefaultFilterOperations.Equals).Type<StringType>();
        descriptor.Operation(DefaultFilterOperations.StartsWith).Type<StringType>();
        descriptor.Operation(DefaultFilterOperations.Contains).Type<StringType>();
    }
}
