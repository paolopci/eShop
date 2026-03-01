using Npgsql;

var list = await Cli("dotnet", "user-secrets list --project eShop.Catalog.API");
var line = list.Split('\n', StringSplitOptions.RemoveEmptyEntries)
    .Select(x => x.Trim())
    .FirstOrDefault(x => x.StartsWith("ConnectionStrings:CatalogDB = ", StringComparison.Ordinal));

if (line is null)
{
    Console.WriteLine("SECRET_MISSING");
    return 1;
}

var cs = line[(line.IndexOf("=", StringComparison.Ordinal) + 2)..];

await using var conn = new NpgsqlConnection(cs);
await conn.OpenAsync();

await PrintCount(conn, "Brands");
await PrintCount(conn, "ProductTypes");
await PrintCount(conn, "Products");

return 0;

static async Task PrintCount(NpgsqlConnection conn, string table)
{
    await using var cmd = new NpgsqlCommand($"select count(*) from \"{table}\"", conn);
    var count = (long)(await cmd.ExecuteScalarAsync() ?? 0L);
    Console.WriteLine($"COUNT[{table}]={count}");
}

static async Task<string> Cli(string fileName, string args)
{
    var psi = new System.Diagnostics.ProcessStartInfo(fileName, args)
    {
        RedirectStandardOutput = true,
        RedirectStandardError = true,
        UseShellExecute = false,
        CreateNoWindow = true
    };

    using var p = new System.Diagnostics.Process { StartInfo = psi };
    p.Start();
    var stdOut = await p.StandardOutput.ReadToEndAsync();
    var stdErr = await p.StandardError.ReadToEndAsync();
    await p.WaitForExitAsync();
    if (p.ExitCode != 0) throw new InvalidOperationException(stdErr);
    return stdOut;
}
