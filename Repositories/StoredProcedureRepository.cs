using CustomerManagerAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class StoredProcedureRepository
    {
        // Application context
        private readonly DummyContext _myAppContext;

        // Inject context to access DB
        public StoredProcedureRepository(DummyContext context)
        {
            _myAppContext = context;
        }

        // To get the FromSql command, you need to add the reference of “Microsoft.EntityFrameworkCore

        public async Task<IEnumerable<Customer>> AddCustomer(Customer customer)
        {
            // await --> AsyncToList is Async, so need to await it
            var response = await _myAppContext.Customer
                           .FromSql("EXEC SetCustomer {0},{1},{2},{3},{4},{5}",
                           customer.FirstName, customer.LastName, customer.City,
                           customer.Country, customer.Phone, customer.Created).ToListAsync();

            return response;
        }

    }
}
