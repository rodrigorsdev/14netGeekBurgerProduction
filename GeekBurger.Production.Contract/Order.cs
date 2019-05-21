using System;
using System.Collections.Generic;
using System.Text;

namespace GeekBurger.Production.Contract
{
    public class Order
    {
        public Guid TransactionId { get; set; }
        public Guid OrderId { get; set; }
        public Guid StoreId { get; set; }
        public decimal Total { get; set; }
        //public ICollection<Product> Products { get; set; }
        public ICollection<Guid> ProductionIds { get; set; }
        public string State { get; set; } //Paid, Canceled, Finished
    }
}
