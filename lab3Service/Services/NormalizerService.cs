using Grpc.Core;
using lab3Service.Protos;
using System.Transactions;
using System.Xml.Schema;

namespace lab3Service.Services
{
    public class NormalizerService : Normalizer.NormalizerBase
    {
        lab3.dbNormalizer normalizer = new lab3.dbNormalizer();
        public override async Task<Response> Normalize(IAsyncStreamReader<Request> request, ServerCallContext context)
        {
            int count = 0;
            await foreach(Request request1 in request.ReadAllAsync())
            {
                count++;
                lab3.dbItem.dbItem item = new lab3.dbItem.dbItem(request1.ShopName, request1.ShopAddress, request1.ClientName, request1.ClientEmail,
                                                                request1.ClientPhone, request1.ClientName, request1.DistrName, request1.ItemId,
                                                                request1.ItemName, request1.Amount, Convert.ToDecimal(request1.Price), request1.PurchDate.ToDateTime());

                _ = normalizer.insertRow(item);
            }

            return new Response { Result = $"Done! Rows received: {count}" };
        }
    }
}
