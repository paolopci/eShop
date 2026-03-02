using System;
using System.Reflection;
using HotChocolate.Types.Descriptors;

namespace eShop.Catalog.API.Types;

public static class UseToUpperObjectFieldDescriptorExtensions
{
    public static IObjectFieldDescriptor UseToUpper(this IObjectFieldDescriptor descriptor)
    {
        return descriptor.Use(next => async context =>
        {
            await next(context);

            if (context.Result is string s)
            {
                context.Result = s.ToUpperInvariant();
            }
        });
    }
}

public class UseToUpperAttribute : ObjectFieldDescriptorAttribute
{
    protected override void OnConfigure(IDescriptorContext context,
                                        IObjectFieldDescriptor descriptor,
                                        MemberInfo member)
    => descriptor.UseToUpper();
}
