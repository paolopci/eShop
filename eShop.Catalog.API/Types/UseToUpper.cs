using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using HotChocolate.Types.Descriptors;

namespace eShop.Catalog.API.Types;

// Estensioni per collegare middleware custom ai campi GraphQL.
public static class UseToUpperObjectFieldDescriptorExtensions
{
    // Registra un middleware che intercetta il risultato del resolver.
    public static IObjectFieldDescriptor UseToUpper(this IObjectFieldDescriptor descriptor)
    {
        // La pipeline esegue prima il resolver successivo e poi applica la trasformazione.
        return descriptor.Use(next => async context =>
        {
            // Esegue il middleware/risolutore successivo nella pipeline GraphQL.
            await next(context);

            // Se il risultato del campo e' una stringa, la converte in maiuscolo in modo culture-invariant.
            if (context.Result is string s)
            {
                context.Result = s.ToUpperInvariant();
            }
        });
    }
}

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
