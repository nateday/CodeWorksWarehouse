using CodeWorksWarehouse.Common.Entities;
using CodeWorksWarehouse.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeWorksWarehouse.Business.Services
{
    public class OrderService
    {
        private readonly IOrdersRepository _orderRepo;

        public OrderService(IOrdersRepository orderRepo)
        {
            _orderRepo = orderRepo;
        }

        public IEnumerable<IOrder> GetUnprocessedOrders()
        {
            var orders = _orderRepo.GetUnProcessedOrders().ToList();

            if (orders == null || orders.Count == 0)
            {
                throw new Exception("No orders found.");
            }

            return orders;
        }

        public IEnumerable<IOrder> GetOrders(Guid productId)
        {
            var orders = _orderRepo.GetOrdersByProductId(productId).ToList();

            if (orders == null || orders.Count == 0)
            {
                throw new Exception("No orders found.");
            }

            return orders;
        }

        public IOrder GetOrder(Guid id)
        {
            var order = _orderRepo.GetOrderById(id);

            if (order == null)
            {
                throw new Exception("Order could not be found.");
            }

            return order;
        }

        public IOrder CreateOrder(Order order)
        {
            if (order.Id != Guid.Empty)
            {
                throw new Exception("Order already exists.");
            }

            var newOrder = _orderRepo.CreateOrder(order);

            return newOrder;
        }

        public void UpdateOrder(Order order)
        {
            if (order.ProcessedAt != null)
            {
                throw new Exception("A processed order cannot be updated.");
            }

            if (order.Id == Guid.Empty)
            {
                throw new Exception("Order does not exist.");
            }

            _orderRepo.UpdateOrder(order);
        }

        public IOrder ProcessOrder(Guid id)
        {
            var currentOrder = GetOrder(id);

            if (currentOrder == null)
            {
                throw new Exception("Order not found.");
            }

            if (currentOrder.ProcessedAt != null)
            {
                throw new Exception("Order has already been processed.");
            }

            currentOrder.ProcessedAt = DateTimeOffset.Now;
            _orderRepo.UpdateOrder((Order)currentOrder);

            return currentOrder;
        }
    }
}
