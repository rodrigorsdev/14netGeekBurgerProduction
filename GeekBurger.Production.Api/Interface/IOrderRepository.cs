using GeekBurger.Production.Models;
using System;
using System.Threading.Tasks;

namespace GeekBurger.Production.Interface
{
    /// <summary>
    /// Order repository interface
    /// </summary>
    public interface IOrderRepository
    {
        Task<Order> GetById(Guid id);
        Task Add(Order model);
        Task Update(Order model);
    }
}
