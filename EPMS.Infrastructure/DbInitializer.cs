using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EPMS.Infrastructure;

public static class DbInitializer
{
    public static void InitializeDatabase(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var configuration = services.GetRequiredService<IConfiguration>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(connectionString))
        {
            return;
        }

        var sqlScriptsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "EPMS.Infrastructure", "SqlScripts");
        
        if (!Directory.Exists(sqlScriptsPath))
        {
            sqlScriptsPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "EPMS.Infrastructure", "SqlScripts");
        }

        if (!Directory.Exists(sqlScriptsPath))
        {
             var current = Directory.GetCurrentDirectory();
             while (current != null && !Directory.Exists(Path.Combine(current, "EPMS.Infrastructure", "SqlScripts")))
             {
                 current = Directory.GetParent(current)?.FullName;
             }
             if (current != null)
             {
                 sqlScriptsPath = Path.Combine(current, "EPMS.Infrastructure", "SqlScripts");
             }
        }

        if (!Directory.Exists(sqlScriptsPath))
        {
            Console.WriteLine($"SQL Scripts directory not found at {sqlScriptsPath}");
            return;
        }

        var scriptFiles = Directory.GetFiles(sqlScriptsPath, "*.sql").OrderBy(f => f);

        using var connection = new SqlConnection(connectionString);
        connection.Open();

        foreach (var file in scriptFiles)
        {
            var script = File.ReadAllText(file);
            
            // Split script by GO command on its own line
            var commands = Regex.Split(script, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);

            foreach (var commandText in commands)
            {
                if (string.IsNullOrWhiteSpace(commandText)) continue;

                using var command = new SqlCommand(commandText, connection);
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Error executing command from {Path.GetFileName(file)}: {ex.Message}");
                }
            }
            Console.WriteLine($"Processed script: {Path.GetFileName(file)}");
        }
    }
}
