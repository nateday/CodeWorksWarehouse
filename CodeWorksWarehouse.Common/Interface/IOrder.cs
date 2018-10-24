using System;

namespace CodeWorksWarehouse.Common.Interface
{
    public interface IOrder
    {
        Guid Id { get; set; }
        Guid ProductId { get; set; }
        DateTimeOffset CreatedAt { get; set; }
        DateTimeOffset? ProcessedAt { get; set; }
        bool RemoveStock { get; set; }
        int Stock { get; set; }
    }
}
