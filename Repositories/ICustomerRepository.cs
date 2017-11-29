using CustomerManagerAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories
{
    public interface ICustomerRepository: IRepository<Customer>
    {
        IEnumerable<Customer> GetAllCustomersAndOrderRepo();
        
    }
}
