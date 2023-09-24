﻿using MediatR;
using Ordering.Application.Features.Order.Queries.GetOrdersList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Order.Queries.GetOrderList
{
    public class GetOrdersListQuery:IRequest<List<OrdersVm>>
    {
        public string UserName { get; set; }
        public GetOrdersListQuery(string userName)
        {
            UserName = userName;
        }
    }
}
