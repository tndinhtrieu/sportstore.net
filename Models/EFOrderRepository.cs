using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class EFOrderRepository : IOrderRepository
    {
        Models.ApplicationDbContext dbContext;
        public EFOrderRepository(Models.ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<Order> Orders => dbContext.Orders
                                         .Include(o => o.Lines)
                                         .ThenInclude(l => l.Product);
        public void SaveOrder(Order order)
        {
            dbContext.AttachRange(order.Lines.Select(l => l.Product));
            if (order.OrderID == 0)
            {
                dbContext.Orders.Add(order);
            }
            dbContext.SaveChanges();
        }
    }
}
