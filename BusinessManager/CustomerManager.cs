using CustomerManagerAPI.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UoW;

namespace BusinessManager
{
    public class CustomerManager
    {
        public readonly UnitOfWork _unitOfWork;
        public readonly StoredProcedureRepository _storedProcedureRepository;

        public CustomerManager(UnitOfWork unitOfWork, StoredProcedureRepository storedProcedureRepository )
        {
            _unitOfWork = unitOfWork;
            _storedProcedureRepository = storedProcedureRepository;
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


        public async Task<IEnumerable<Customer>> addCustomerStoredProcedure(Customer customer)
        {
            customer.Created = DateTime.Now;

            var response = await _storedProcedureRepository.AddCustomer(customer);

            return response; // return the Created user
        }


    }
}
