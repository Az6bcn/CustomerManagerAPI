
using CustomerManagerAPI.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore; // necessary to use eager loading
using System.Linq;

namespace Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        //Call the BASE (Parent) class (IRepository) constructor passing it "context"
        public CustomerRepository(DummyContext myAppContext): base(myAppContext) { }


        public DummyContext CustomerContextApp
        {
            /*Cast the Context we re inheriting from the Generic Repository to the name 
             * of our Application's Context (ContextCllassName)-- the context that access the DB. 
             *So we can get Access to our Application Context DbSets */
            get { return CustomContextForQuery as DummyContext; }
        }


        public IEnumerable<Customer> GetAllCustomersAndOrderRepo()
        {
            var customersAndOrder = CustomerContextApp.Customer
                .Include(cus => cus.Order)
                .Where(customer => customer.Deleted == null)
                .ToList();
            return customersAndOrder;
        }


       


    }

}
