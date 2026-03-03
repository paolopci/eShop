using System.Reflection;
using System.Runtime.CompilerServices;
using HotChocolate.Types.Descriptors;

namespace eShop.Catalog.API.Types.Configuration
{
    public class UseToUpperAttribute : ObjectFieldDescriptorAttribute
    {

        // Il costruttore imposta l'ordine dell'attributo nella pipeline.
        // CallerLineNumber valorizza automaticamente 'order' con la riga del chiamante
        // quando non viene passato esplicitamente.
        public UseToUpperAttribute([CallerLineNumber] int order = default)
        {
            Order = order;
        }

        // Hook di HotChocolate: applica l'estensione UseToUpper al campo decorato.
        protected override void OnConfigure(IDescriptorContext context,
                                        IObjectFieldDescriptor descriptor,
                                        MemberInfo member)
    => descriptor.UseToUpper();
    }
}
