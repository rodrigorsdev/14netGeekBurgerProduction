using System.Collections.Generic;

using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Hosting;

using GeekBurger.Production.Contract;
using GeekBurger.Production.Models;

namespace GeekBurger.Production.Application.Interfaces
{
    /// <summary>
    /// Order Service Interface
    /// </summary>
    public interface INewOrderService: IHostedService
    {
        /// <summary>
        /// Send messages asynchronously
        /// </summary>
        void SendMessagesAsync();

        /// <summary>
        /// Add to message list
        /// </summary>
        /// <param name="changes">IEnumerable of EntityEntry<![CDATA[Order]]></param>
        void AddToMessageList(IEnumerable<EntityEntry<Order>> changes);
    }
}
