using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekBurger.Production.Application.ViewModel
{
    public class NewOrder
    {
        public Guid OrderId { get; set; }
        public Guid StoreId { get; set; }
        public decimal Total { get; set; }
        public ICollection<NewOrderProduct> Products { get; set; }
        public ICollection<Guid> ProductionIds { get; set; }
    }
}
