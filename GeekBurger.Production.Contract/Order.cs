using System;
using System.Collections.Generic;

using GeekBurger.Products.Contract;

namespace GeekBurger.Production.Contract
{
    /// <summary>
    /// Order
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Transaction id
        /// </summary>
        public Guid TransactionId { get; set; }

        /// <summary>
        /// Order id
        /// </summary>
        public Guid OrderId { get; set; }

        /// <summary>
        /// Store id
        /// </summary>
        public Guid StoreId { get; set; }

        /// <summary>
        /// Total
        /// </summary>
        public decimal Total { get; set; }

        /// <summary>
        /// Products
        /// </summary>
        public ICollection<ProductToGet> Products { get; set; }

        /// <summary>
        /// Production ids
        /// </summary>
        public ICollection<Guid> ProductionIds { get; set; }

        /// <summary>
        /// Order state
        /// </summary>
        public string State { get; set; } //Paid, Canceled, Finished
    }
}
