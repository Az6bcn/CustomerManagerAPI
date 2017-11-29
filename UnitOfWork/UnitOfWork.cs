using CustomerManagerAPI.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace UoW
{
    public class UnitOfWork
    {
        // Application context
        private readonly DummyContext _myAppContext;
        
        public UnitOfWork(DummyContext context)
        {
            _myAppContext = context;

            //Initialise our specific model Repositories
            Customer = new CustomerRepository(_myAppContext);
            Order = new OrderRepository(_myAppContext);
            Product = new ProductRepository(_myAppContext);
        }

        //properties 
        public ICustomerRepository Customer { get; private set; }
        public IOrderRepository Order { get; private set; }
        public IProductRepository Product { get; }

        //Calls the SaveChanges on the Context
        public int Complete()
        {
            return _myAppContext.SaveChanges();
        }


        //Implementation of the Dispose Method to Dispose the Context
        public void Dispose()
        {
            _myAppContext.Dispose();
        }
    }
}

