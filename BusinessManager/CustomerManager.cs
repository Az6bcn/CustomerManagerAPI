using CustomerManagerAPI.Models;
using Model.Enumerations;
using Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using UoW;

namespace BusinessManager
{
    public class CustomerManager
    {
        public readonly UnitOfWork _unitOfWork;
        public readonly StoredProcedureRepository _storedProcedureRepository;
        private readonly BasePerson _basePerson;
        public Guid UpdatedByPersonID { get; set; } // of person that creates
        public Guid SourcePersonID { get; set; } // of person that update
        public ManagerRolesEnum PersonRole { get; set; }


        public CustomerManager(UnitOfWork unitOfWork, StoredProcedureRepository storedProcedureRepository,
            BasePerson basePerson)
        {
            _unitOfWork = unitOfWork;
            _storedProcedureRepository = storedProcedureRepository;

            _basePerson = basePerson;

            UpdatedByPersonID = _basePerson.CurrentPersonID();
            SourcePersonID = _basePerson.CurrentPersonID();
            PersonRole = _basePerson.CurrentPersonRole();

        }

        public IEnumerable<Customer> GetAllCustomer()
        {
            var response = _unitOfWork.Customer.GetAll();

            return response;
        }


        public IEnumerable<Customer> GetCustomersAndOrder()
        {
            var response = _unitOfWork.Customer.GetAllCustomersAndOrderRepo();

            return response;
        }
     
        public Customer addCustomer(Customer customer)
        {
            customer.Created = DateTime.Now;

            _unitOfWork.Customer.addCustomerToDB(customer);
            _unitOfWork.Complete();
            _unitOfWork.Dispose();

            return customer; // return the Created user
        }

        ///*************************Stored Procedure*********************************************/

        /// <summary>
        /// Get list of customers approved by General Manager
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Customer>> GetAllCustomersApprovedByGeneralManagerAsync()
        {
            var response = await _storedProcedureRepository.GetAllCustomersApprovedByGeneralManagerAsync();
            return response;
        }

        /// <summary>
        /// Get list of customers created by General Manager
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Customer>> GetAllCustomerCreatedByGeneralManagerAsync()
        {
            var response = await _storedProcedureRepository.GetAllCustomerCreatedByGeneralManagerAsync();

            return response;
        }
        

        /// <summary>
        /// Get list of customers created by Customer Manager
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Customer>> GetAllCustomersApprovedByCustomerManagerAsync()
        {
            var response = await _storedProcedureRepository.GetAllCustomersApprovedByCustomerManagerAsync();

            return response;
        }

        /// <summary>
        /// Get list of customers created by Product Manager
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Product>> GetAllProductsApprovedByGeneralManagerAsync()
        {
            var response = await _storedProcedureRepository.GetAllProductsApprovedByGeneralManagerAsync();

            return response;
        }
        


        /// <summary>
        /// Get list of customers created by Product Manager
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Customer>> GetAllCustomerCreatedBySectionManagerAsync()
        {
            var response = await _storedProcedureRepository.GetAllCustomerCreatedBySectionManagerAsync();

            return response;
        }


        /// <summary>
        /// creates customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Customer>> addCustomerStoredProcedure(Customer customer)
        {
            customer.Created = DateTime.Now;

            var response = await _storedProcedureRepository.AddCustomer(customer, PersonRole, SourcePersonID);

            return response; // return the Created user
        }


        /// <summary>
        /// Updates Customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public async Task<Customer> updateCustomerStoredProcedureAsync(Customer customer)
        {
            var response = await _storedProcedureRepository.updateCustomerStoredProcedureRepositoryAsync(customer, UpdatedByPersonID);
            return response;
        }

        /// <summary>
        /// Deletes a Customer
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public async Task<Customer> deleteCustomer(int customerID)
        {
            var response = await _storedProcedureRepository.updateCustomer(customerID);
            return response;
        }

        /// <summary>
        /// set a customer as approved
        /// </summary>
        /// <param name="cus"></param>
        /// <returns></returns>
        public async Task<bool> ApproveCustomer(Customer cus)
        {
            var response = await _storedProcedureRepository.ApproveCustomer(cus.Id);

            return response;
        }

        /// <summary>
        /// set a product as approved
        /// </summary>
        /// <param name="cus"></param>
        /// <returns></returns>
        public async Task<bool> ApproveProduct(Product product)
        {
            var response = await _storedProcedureRepository.ApproveProduct(product.Id);

            return response;
        }
    }
}
