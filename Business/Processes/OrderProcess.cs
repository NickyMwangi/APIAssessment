using Business.IProcesses;
using Data.DBContext;
using Data.Entities;
using Data.Interfaces;
using Library.Common;
using Library.Dtos;
using Library.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Business.Processes;

public class OrderProcess : BaseProcess<Order, Order_items, OrderDto, OrderLineDto>, IOrderProcess
{
    private readonly Db db;
    public OrderProcess(IRepoService repo, IAppSettings appSetting, IMapperService mapper, Db _db) : base(repo, appSetting, mapper)
    {
        db = _db;
    }

    public override IEnumerable<Order> FilterQuery(string filterType)
    {
        var orders = repo.GetAll<Order>();
        return base.FilterQuery(filterType);
    }

    public JsonBody<OrderDto, OrderLineDto> CitySales(int perPage, int page)
    {
        var orderItems = db.order_items.Include(n => n.Product).Include(r => r.Header).ToList();
        var mappedData = mapper.MapConfig<List<Order_items>, List<OrderLineDto>>(orderItems);

        var orders = db.orders.Include(r => r.Customer).ToList();
        var mappedOrders = mapper.MapConfig<List<Order>, List<OrderDto>>(orders);

        mappedData.ForEach(o => {
            var order = mappedOrders.First(r => r.Order_id == o.Order_id);
            o.customer = order.Customer;
        });


        var groupByCity = mappedData.GroupBy(r => r.customer.City).ToList();
        var resp = new JsonBody<OrderDto, OrderLineDto>
        {
            Message = "Success",
            IsSuccess = true,
            Lines = mappedData
        };
        resp.Metadata.PerPage = perPage;
        resp.Metadata.Page = page;
        resp.Metadata.TotalItems = db.orders.Where(n => n.Shipped_date < n.Required_date).Count();
        return resp;
    }

    public JsonBody<OrderDto, OrderLineDto> EarlyDelivery(int perPage, int page)
    {
        var data = db.orders.Include(r => r.Customer)
             .Include(t => t.Store).Include(b => b.Staff)
             .Where(n => n.Shipped_date < n.Required_date).Skip(perPage * (page - 1)).Take(perPage).ToList();
        var mappedData = mapper.MapConfig<List<Order>, List<OrderDto>>(data);
        var resp = new JsonBody<OrderDto, OrderLineDto>
        {
            Message = "Success",
            IsSuccess = true,
            Data = mappedData
        };
        resp.Metadata.PerPage = perPage;
        resp.Metadata.Page = page;
        resp.Metadata.TotalItems = db.orders.Where(n => n.Shipped_date < n.Required_date).Count();
        return resp;
    }

    public JsonBody<OrderDto, OrderLineDto> LateDelivery(int perPage, int page)
    {
        var data = db.orders.Include(r => r.Customer)
            .Include(t => t.Store).Include(b => b.Staff)
            .Where(n => n.Shipped_date > n.Required_date)
            .Skip(perPage * (page - 1)).Take(perPage).ToList();

        var mappedData = mapper.MapConfig<List<Order>, List<OrderDto>>(data);
        var resp = new JsonBody<OrderDto, OrderLineDto>
        {
            Message = "Success",
            IsSuccess = true,
            Data = mappedData
        };
        resp.Metadata.PerPage = perPage;
        resp.Metadata.Page = page;
        resp.Metadata.TotalItems = db.orders.Where(n => n.Shipped_date > n.Required_date).Count();
        return resp;
    }

    public JsonBody<OrderDto, OrderLineDto> CombinedDelivery(int perPage, int page)
    {
        var data = db.orders.Include(r => r.Customer)
            .Include(t => t.Store).Include(b => b.Staff)
            .Skip(perPage * (page - 1)).Take(perPage).ToList();

        var mappedData = mapper.MapConfig<List<Order>, List<OrderDto>>(data);
        foreach (var n in mappedData)
        {
            if (n.Shipped_date > n.Required_date)
                n.Delivery = "Late";
            else
                n.Delivery = "Early";
        }
        var resp = new JsonBody<OrderDto, OrderLineDto>
        {
            Message = "Success",
            IsSuccess = true,
            Data = mappedData
        };
        resp.Metadata.PerPage = perPage;
        resp.Metadata.Page = page;
        resp.Metadata.TotalItems = db.orders.Count();
        return resp;
    }
    public JsonBody<OrderDto, OrderLineDto> TimeCheck(string CurrTime, string TimeText)
    {

        var resp = new JsonBody<OrderDto, OrderLineDto>
        {
            Message = "I was called at" + CurrTime,
            IsSuccess = true,
            Data = []
        };
        return resp;
    }
}
