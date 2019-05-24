using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekBurger.Production.Application.Interfaces
{
    public interface IOrderService: IHostedService
    {
        void ProductionAreaChanged();
        void NewOrder();
        void OrderChanged();
        void WaitOrderChanged();
    }
}
