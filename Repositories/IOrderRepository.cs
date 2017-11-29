using CustomerManagerAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetCustomerAndOrdersAndOrderItemsRepo();
    }
}
