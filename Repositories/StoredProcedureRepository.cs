using CustomerManagerAPI.Models;
using Microsoft.EntityFrameworkCore;
using Model.Enumerations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

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

        public async Task<IEnumerable<Customer>> GetAllCustomersApprovedByGeneralManagerAsync()
        {
            var response = await _myAppContext.Customer
                        .FromSql("EXEC spd_GetAllCustomersApprovedByGeneralManager")
                        .ToListAsync();

            return response;
        }



        public async Task<IEnumerable<Customer>> GetAllCustomerCreatedByGeneralManagerAsync()
        {
            var response = await _myAppContext.Customer
                            .FromSql("EXEC spd_GetAllCustomerCreatedByGeneralManager")
                        .ToListAsync<Customer>();

            return response;
        }
        


        public async Task<IEnumerable<Customer>> GetAllCustomersApprovedByCustomerManagerAsync()
        {
            var response = await _myAppContext.Customer
                            .FromSql("EXEC spd_GetAllCustomerCreatedByCustomerManager")
                        .ToListAsync<Customer>();

            return response;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomerCreatedBySectionManagerAsync()
        {
            var response = await _myAppContext.Customer
                            .FromSql("EXEC spd_GetAllCustomerCreatedBySectionManager")
                            .ToListAsync<Customer>();

            return response;
        }

        public async Task<IEnumerable<Product>> GetAllProductsApprovedByGeneralManagerAsync()
        {
            var response = await _myAppContext.Product
                            .FromSql("EXEC spd_GetAllProductsCreatedByProductManager")
                            .ToListAsync<Product>();

            return response;
        }


        public async Task<IEnumerable<Customer>> AddCustomer(Customer customer, ManagerRolesEnum CreatedByRole, Guid SourcePersonID)
        {
            // await --> AsyncToList is Async, so need to await it
            var response = await _myAppContext.Customer
                           .FromSql("EXEC SetCustomer {0},{1},{2},{3},{4},{5},{6},{7}",
                           customer.FirstName, customer.LastName, customer.City,
                           customer.Country, customer.Phone, customer.Created, CreatedByRole, SourcePersonID).ToListAsync();

            return response;
        }


        public async Task<Customer> updateCustomerStoredProcedureRepositoryAsync(Customer customer, Guid UpdatedByPersonID) {

            var response = await _myAppContext.Customer
                        .FromSql("EXEC SetUpdateCustomer {0},{1},{2},{3},{4},{5},{6},{7}",
                        customer.Id, customer.FirstName, customer.LastName, customer.City, customer.Country, customer.Phone,
                        customer.ApprovedByGeneralManager, UpdatedByPersonID)
                        .FirstAsync();

            return response;
            
        }


        public async Task<Customer> updateCustomer(int customerID)
        {
            var response = await _myAppContext.Customer
                        .FromSql("EXEC SetDeleteCustomer {0}", customerID)
                        .FirstAsync();

            return response;

        }

        
        /// <summary>
        /// set customer as approved
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public async Task<bool> ApproveCustomer(int customerID)
        {
            dynamic response;
            using (var command = _myAppContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SetApproveCustomer"; // stored procedure name
                command.CommandType = CommandType.StoredProcedure;
                // stored procedure parameter
                command.Parameters.Add(new SqlParameter("@customerID", customerID));
                

                _myAppContext.Database.OpenConnection();
               
                using (var result = command.ExecuteReader())
                {
                    if (result.HasRows)
                    {
                        // read the result
                        while (result.Read())
                        {
                            // get the returned column value
                            response = result["BOOLRESULT"];
                            return response;
                        }
                    }
                }

                return false; ;
            }
            
        }




        /// <summary>
        /// set product as approved
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public async Task<bool> ApproveProduct(int productID)
        {
            dynamic response;
            using (var command = _myAppContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SetApproveProduct"; // stored procedure name
                command.CommandType = CommandType.StoredProcedure;
                // stored procedure parameter
                command.Parameters.Add(new SqlParameter("@cproductID", productID));


                _myAppContext.Database.OpenConnection();

                using (var result = command.ExecuteReader())
                {
                    if (result.HasRows)
                    {
                        // read the result
                        while (result.Read())
                        {
                            // get the returned column value
                            response = result["BOOLRESULT"];
                            return response;
                        }
                    }
                }

                return false; ;
            }

        }


    }
}
