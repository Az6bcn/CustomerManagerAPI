using Repositories;
using System;

namespace UoW
{
    public interface IUnitOfWork: IDisposable
    {
        //Exposes our specific model Repositories Interfaces, each one implements the IRepository.
        ICustomerRepository Customer { get; }
        IOrderRepository Order { get; }
        IProductRepository Product { get; }

        int Complete();
    }
}
