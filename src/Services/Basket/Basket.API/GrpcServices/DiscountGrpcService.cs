using System.Net;
using Discount.Grpc.Protos;
using static Discount.Grpc.Protos.DiscountProtoService;

namespace Basket.API.GrpcServices
{
    public class DiscountGrpcService
	{
		private readonly DiscountProtoServiceClient _discountProtoServiceClient;

        public DiscountGrpcService(DiscountProtoServiceClient discountProtoServiceClient)
        {
            _discountProtoServiceClient = discountProtoServiceClient;
        }

        public async Task<CouponModel> GetDiscount(string productName)
        {
            return await _discountProtoServiceClient.GetDiscountAsync(new GetDiscountRequest { ProductName = productName });
        }
    }
}

  