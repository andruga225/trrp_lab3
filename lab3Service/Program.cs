using lab3Service.Services;

namespace lab3Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddGrpc();

            var app = builder.Build();

            app.MapGrpcService<NormalizerService>();
            //app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}