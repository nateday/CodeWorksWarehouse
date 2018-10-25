using CodeWorksWarehouse.Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeWorksWarehouse.Data
{
    public static class FakeData
    {
        public static List<Order> Orders = new List<Order>()
        {
            new Order()
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTimeOffset.Now.AddDays(-1),
                RemoveStock = false,
                Stock = 1
            },
            new Order()
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTimeOffset.Now.AddDays(-3),
                RemoveStock = false,
                Stock = 5
            },
            new Order()
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTimeOffset.Now.AddDays(-2),
                RemoveStock = true,
                Stock = 10
            },
            new Order()
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTimeOffset.Now.AddDays(-1),
                RemoveStock = false,
                Stock = 15
            },
        };
    }
}
