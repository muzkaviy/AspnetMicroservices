using System;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;
using Mapster;
using MapsterMapper;

namespace Discount.Grpc.Services
{
	public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
	{
		private readonly IDiscountRepository discountRepository;
		private readonly ILogger<DiscountService> logger;

        public DiscountService(ILogger<DiscountService> logger, IDiscountRepository discountRepository)
        {
            this.logger = logger;
            this.discountRepository = discountRepository;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await discountRepository.GetDiscount(request.ProductName);

            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with product name={request.ProductName}"));
            }

            return coupon.Adapt<CouponModel>();
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            var success = await discountRepository.CreateDiscount(coupon);

            if (!success)
            { 
                throw new RpcException(new Status(StatusCode.Aborted, $"Discount with product name={request.Coupon.ProductName} could not be created."));
            }

            return request.Coupon;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            var success = await discountRepository.UpdateDiscount(coupon);

            if (!success)
            {
                throw new RpcException(new Status(StatusCode.Aborted, $"Discount with product name={request.Coupon.ProductName} could not be updated."));
            }

            return request.Coupon;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            return new DeleteDiscountResponse { Success = await discountRepository.DeleteDiscount(request.ProductName) };
        }
    }
}

