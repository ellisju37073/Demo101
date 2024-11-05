using Microsoft.Extensions.Configuration;
using System.Data.Common;
using Microsoft.Data.SqlClient;


namespace FactoryModel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n Factory Model Example");

            var (provider, connectionString) = GetProviderFromConfiguation();
            Console.WriteLine($"Provider: {provider}");
            Console.WriteLine($"ConnectionString: {connectionString}");


        }
        static (string Provider, string ConnectionString)

            GetProviderFromConfiguation()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            var providerName = configuration["ProviderName"];
            var connectionString = configuration[$"{providerName}:ConnectionString"];
            return (providerName, connectionString);
        }
}
}
