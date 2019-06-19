using GeekBurger.Production.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekBurger.Production.Interface
{
    public interface IOrderRepository
    {
        Task<Order> GetById(Guid id);
        Task Add(Order model);
        Task Update(Order model);
    }
}
