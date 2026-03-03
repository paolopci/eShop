using System;

namespace eShop.Catalog.API.Types.Configuration;

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
