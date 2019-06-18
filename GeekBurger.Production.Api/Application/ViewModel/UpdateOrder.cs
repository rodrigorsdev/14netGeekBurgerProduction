using System;

namespace GeekBurger.Production.Application.ViewModel
{
    public class UpdateOrder
    {
        public Guid OrderId { get; set; }
        public Guid StoreId { get; set; }
        public OrderState State { get; set; }
    }
}
