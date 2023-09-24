using AutoMapper;
using Ordering.Application.Features.Order.Commands.CheckOutOrder;
using Ordering.Application.Features.Order.Commands.UpdateOrder;
using Ordering.Application.Features.Order.Queries.GetOrdersList;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Order,OrdersVm>().ReverseMap();
            CreateMap<Order,CheckOutOrderCommand>().ReverseMap(); 
            CreateMap<Order,UpdateOrderCommand>().ReverseMap();
        }
    }
}
