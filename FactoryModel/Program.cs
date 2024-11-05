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

            DbProviderFactory factory = GetDbProviderFactory(provider);

            using (DbConnection connection = factory.CreateConnection())
            {
               Console.WriteLine($"Connection Type: {connection.GetType().Name}");

               connection.ConnectionString = connectionString;
               connection.Open();

                DbCommand command = factory.CreateCommand();
                Console.WriteLine($"Command Type: {command.GetType().Name}");

                command.Connection = connection;

                command.CommandText = "Select i.Id, m.Name From Inventory i inner join Makes m on m.Id = i.MakeId";
                using (DbDataReader reader = command.ExecuteReader())
                {
                  Console.WriteLine($"Id \t Your data reader object is a : {reader.GetType().Name}");

                  Console.WriteLine("\n\t *** Current Inventory ***");

                    while(reader.Read()) {
                        Console.WriteLine($"\t-> Car # {reader["Id"]} is a {reader["Name"]}");
                    }
                }
            }


        }

        static DbProviderFactory GetDbProviderFactory(string provider)
        {
            if (provider == "SqlServer")
            {
                return SqlClientFactory.Instance;
            }
            else return null;
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
