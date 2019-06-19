using GeekBurger.Production.Contract;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;

namespace GeekBurger.Production.Application.Interfaces
{
    /// <summary>
    /// Order Service Interface
    /// </summary>
    public interface INewOrderService: IHostedService
    {
        void SendMessagesAsync();
        void AddToMessageList(IEnumerable<EntityEntry<GeekBurguer.Orders.Contract.NewOrderMessage>> changes);
    }
}
