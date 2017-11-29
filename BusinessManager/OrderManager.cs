using CustomerManagerAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UoW;

namespace BusinessManager
{
    public class OrderManager
    {

        public readonly UnitOfWork _unitOfWork;

        public OrderManager(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Customer> GetAllOrders()
        {
            var response = _unitOfWork.Customer.GetAll();

            return response;
        }


        public IEnumerable<Order> GetCustomerAndOrdersAndOrderItems()
        {
            var response = _unitOfWork.Order.GetCustomerAndOrdersAndOrderItemsRepo();

            return response;
        }
    }
}
