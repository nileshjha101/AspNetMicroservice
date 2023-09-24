using AutoMapper;
using Discount.Grpc.Protos;
using Discount.Grpc.Repository;
using Grpc.Core;

namespace Discount.Grpc.Services
{
    public class DiscountService:DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IDiscountRepository _repository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public DiscountService(IDiscountRepository discountRepository,ILogger logger,IMapper mapper)
        {
            _logger = logger;
            _repository = discountRepository;
            _mapper = mapper;
        }
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _repository.GetDiscount(request.ProductName);
            if(coupon is null)
            {
                throw new RpcException(Status.DefaultCancelled);
            }
            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;
        }
    }
}
