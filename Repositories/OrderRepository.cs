using CustomerManagerAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        //Call the BASE (Parent) class (IRepository) constructor passing it "context"
        public OrderRepository(DummyContext myAppContext): base(myAppContext) { }


        public DummyContext CustomerContextApp
        {
            /*Cast the Context we re inheriting from the Generic Repository to the name 
             * of our Application's Context (ContextCllassName)-- the context that access the DB. 
             *So we can get Access to our Application Context DbSets */
            get { return CustomContextForQuery as DummyContext; }
        }


        public IEnumerable<Order> GetCustomerAndOrdersAndOrderItemsRepo()
        {
            var customersAndOrder = CustomerContextApp.Order
                .Include(cus => cus.Customer)
                .Include(cusOrder => cusOrder.OrderItem)
                .ToList();
            return customersAndOrder;
        }

    }
}
