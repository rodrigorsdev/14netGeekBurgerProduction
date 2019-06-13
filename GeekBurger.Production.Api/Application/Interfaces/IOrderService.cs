using Microsoft.Extensions.Hosting;

namespace GeekBurger.Production.Application.Interfaces
{
    /// <summary>
    /// Order Service Interface
    /// </summary>
    public interface IOrderService: IHostedService
    {
        void ProductionAreaChanged();
        void NewOrder();
        void OrderChanged();
        void WaitOrderChanged();
    }
}
