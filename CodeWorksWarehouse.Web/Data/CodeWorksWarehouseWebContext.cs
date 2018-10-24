using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CodeWorksWarehouse.Common.Entities;

namespace CodeWorksWarehouse.Web.Models
{
    public class CodeWorksWarehouseWebContext : DbContext
    {
        public CodeWorksWarehouseWebContext (DbContextOptions<CodeWorksWarehouseWebContext> options)
            : base(options)
        {
        }

        public DbSet<CodeWorksWarehouse.Common.Entities.Order> Order { get; set; }
    }
}
