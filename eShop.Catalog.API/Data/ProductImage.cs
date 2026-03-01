using System;

namespace eShop.Catalog.API.Data;

public sealed record ProductImage(string Name, Func<Stream> OpenStream);