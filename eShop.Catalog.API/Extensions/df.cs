using System;
using HotChocolate.Data.Filters;

namespace eShop.Catalog.API.Extensions;

public class CustomStringOperationFilterInputType : StringOperationFilterInputType
{
    protected override void Configure(IFilterInputTypeDescriptor descriptor)
    {
        descriptor.Operation(DefaultFilterOperations.Equals).Type<StringType>();
        descriptor.Operation(DefaultFilterOperations.StartsWith).Type<StringType>();
    }
}
