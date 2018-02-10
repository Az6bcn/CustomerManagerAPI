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

            Debug.WriteLine("****************--------> {0}, {1}", UpdatedByPersonID, PersonRole);

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


        public async Task<IEnumerable<Customer>> addCustomerStoredProcedure (Customer customer)
        {
            customer.Created = DateTime.Now;

            var response = await _storedProcedureRepository.AddCustomer(customer, PersonRole, SourcePersonID);

            return response; // return the Created user
        }


        public async Task<Customer> updateCustomerStoredProcedureAsync (Customer customer)
        {
            var response = await _storedProcedureRepository.updateCustomerStoredProcedureRepositoryAsync(customer, UpdatedByPersonID);
            return response;
        }


    } 
}
