using CodeWorksWarehouse.Common.Entities;
using System;
using System.Collections.Generic;

namespace CodeWorksWarehouse.Common.Interface
{
    public interface IOrdersRepository
    {
        Order GetOrderById(Guid id);
        void UpdateOrder(Order o);
        IOrder CreateOrder(Order data);
        IEnumerable<Order> GetUnProcessedOrders();
        IEnumerable<Order> GetOrdersByProductId(Guid productId);
    }
}
