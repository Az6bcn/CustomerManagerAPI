using CustomerManagerAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using UoW;

namespace BusinessManager
{
    public class CustomerManager
    {
        public readonly UnitOfWork _unitOfWork;

        public CustomerManager(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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




    }
}
