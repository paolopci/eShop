using System;

namespace eShop.Catalog.API.Models;

public sealed record ProductImage(string Name, Func<Stream> OpenStream);