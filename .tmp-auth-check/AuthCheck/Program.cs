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

var ok = await TryConnect(cs, "primary");
if (!ok)
{
    var b = new NpgsqlConnectionStringBuilder(cs) { Database = "postgres" };
    await TryConnect(b.ConnectionString, "fallback-postgres");
}

return 0;

static async Task<bool> TryConnect(string cs, string label)
{
    try
    {
        await using var conn = new NpgsqlConnection(cs);
        await conn.OpenAsync();
        await using var cmd = new NpgsqlCommand("select current_user, current_database()", conn);
        await using var reader = await cmd.ExecuteReaderAsync();
        await reader.ReadAsync();
        Console.WriteLine($"AUTH_OK[{label}] user={reader.GetString(0)} db={reader.GetString(1)}");
        return true;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"AUTH_FAIL[{label}] {ex.GetType().Name}: {ex.Message}");
        return false;
    }
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
