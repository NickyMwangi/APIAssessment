using Data.Entities;
using Library.Common;
using Library.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IProcesses;

public interface IOrderProcess : IBaseProcess<Order, Order_items, OrderDto, OrderLineDto>
{
    JsonBody<OrderDto, OrderLineDto> CitySales(int perPage, int page);
    JsonBody<OrderDto, OrderLineDto> EarlyDelivery(int perPage, int page);
    JsonBody<OrderDto, OrderLineDto> LateDelivery(int perPage, int page);
    JsonBody<OrderDto, OrderLineDto> CombinedDelivery(int perPage, int page);
    JsonBody<OrderDto, OrderLineDto> TimeCheck(string CurrTime, string TimeText);

}
