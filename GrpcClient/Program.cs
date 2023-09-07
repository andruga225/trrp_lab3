using Grpc.Net.Client;
using GrpcClient.Protos;
using Microsoft.Data.Sqlite;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GrpcClient
{
    internal class Program
    {
        private static string dbPath;
        private static string _connStrSql;

        static async Task<int> Main(string[] args)
        {
            GetDbPath();

            var httpHandler = new HttpClientHandler();
            // Return `true` to allow certificates that are untrusted/invalid
            httpHandler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            using var channel = GrpcChannel.ForAddress($"https://{Config.Default.ip}:{Config.Default.port}",
                new GrpcChannelOptions { HttpHandler = httpHandler});

            // создаем клиент
            var client = new Normalizer.NormalizerClient(channel);

            var call = client.Normalize();

            using (var conn = new SqliteConnection(_connStrSql))
            {
                conn.Open();

                using (var sqlCommand = new SqliteCommand
                {
                    Connection = conn,
                    CommandText = @"SELECT * FROM data"
                })
                {
                    var reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {

                        Request request = new Request
                        {
                            ShopName = (string)reader["shop_name"],
                            ShopAddress = (string)reader["shop_address"],
                            ClientName = (string)reader["client_name"],
                            ClientEmail = (string)reader["client_email"],
                            ClientPhone = (string)reader["client_phone"],
                            CategoryName = (string)reader["category"],
                            DistrName = (string)reader["distr"],
                            ItemId = (int)(long)reader["item_id"],
                            ItemName = (string)reader["item_name"],
                            Amount = (int)(long)reader["amount"],
                            Price = double.Parse((string)reader["price"]),
                            PurchDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTimeOffset(DateTime.Parse((string)reader["purch_date"]))
                        };

                        await call.RequestStream.WriteAsync(request);
                    }
                }

                await call.RequestStream.CompleteAsync();
                Response response = await call.ResponseAsync;
                Console.WriteLine(response.ToString());
                Console.ReadKey();
                return 0;
            }
        }

        private static void GetDbPath()
        {
            Console.WriteLine("Укажите путь до базы данных");
            dbPath = Console.ReadLine();
            dbPath = dbPath.Replace("\"", "");

            _connStrSql = new SqliteConnectionStringBuilder
            {
                DataSource = dbPath,
            }.ConnectionString;
        }
    }
}
