using CodeWorksWarehouse.Common.Interface;
using System;
using System.ComponentModel.DataAnnotations;

namespace CodeWorksWarehouse.Common.Entities
{
    public class Order : IOrder
    {
        public Guid Id { get; set; }

        
        public Guid ProductId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? ProcessedAt { get; set; }
        
        public bool RemoveStock { get; set; }
        
        public int Stock { get; set; }
    }
}
