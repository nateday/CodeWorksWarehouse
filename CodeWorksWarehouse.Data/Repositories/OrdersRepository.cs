using CodeWorksWarehouse.Common.Entities;
using CodeWorksWarehouse.Common.Interface;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;

namespace CodeWorksWarehouse.Data.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        public OrdersRepository(IDbConnection db)
        {
            _db = db;
        }

        IDbConnection _db;

        public IEnumerable<Order> GetUnProcessedOrders()
        {
            //return _db.Query<Order>("SELECT * from orders WHERE IsProcessed = 0");

            return new List<Order>() { new Order() { Id = Guid.NewGuid(), ProductId = Guid.NewGuid(), CreatedAt = DateTimeOffset.Now.AddDays(-5), ProcessedAt = DateTimeOffset.Now.AddDays(-3), RemoveStock = false, Stock = 10 } };
        }

        public IEnumerable<Order> GetOrdersByProductId(Guid productId)
        {
            return _db.Query<Order>("SELECT * from orders WHERE ProductId = @ProductId", new { ProductId = productId });
        }

        public Order GetOrderById(Guid id)
        {
            //return _db.QueryFirstOrDefault<Order>("SELECT * from orders WHERE Id = @Id", new { Id = id });

            return new Order() { Id = Guid.NewGuid(), ProductId = Guid.NewGuid(), CreatedAt = DateTimeOffset.Now.AddDays(-5), ProcessedAt = null, RemoveStock = false, Stock = 10 };
        }

        public IOrder CreateOrder(Order data)
        {
            var newOrder = new Order()
            {
                Id = Guid.NewGuid(),
                ProductId = data.ProductId,
                CreatedAt = DateTimeOffset.Now,
                RemoveStock = data.RemoveStock,
                Stock = data.Stock
            };

            var successful = _db.ExecuteAsync(@"
                INSERT INTO orders 
                (id, productId, createdAt, removeStock, stock)
                VALUES (@Id, @ProductId, @CreatedAt, @RemoveStock, @Stock);
            ", newOrder);

            if (successful.Result == 1)
            {
                return newOrder;
            }

            return null;
        }

        public void UpdateOrder(Order o)
        {
            var existingOrder = GetOrderById(o.Id);

            existingOrder.ProductId = o.ProductId;
            existingOrder.RemoveStock = o.RemoveStock;
            existingOrder.Stock = o.Stock;

            _db.ExecuteAsync(@"
                UPDATE orders SET 
                ProductId = @ProductId, RemoveStock = @RemoveStock, Stock = @Stock 
                WHERE Id = @Id", existingOrder);
        }
    }
}
