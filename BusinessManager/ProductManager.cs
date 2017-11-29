using CustomerManagerAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using UoW;

namespace BusinessManager
{
    public class ProductManager
    {
        public readonly UnitOfWork _unitOfWork;

        public ProductManager(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var response = _unitOfWork.Product.GetAll();

            return response;
        }


        public IEnumerable<Product> GetCustomerAndOrdersAndOrderItems()
        {
            var response = _unitOfWork.Product.GetCustomerAndOrdersAndOrderItems();

            return response;
        }
    }
}
