using System;
using System.Collections.Generic;

namespace GeekBurger.Production.Application.ViewModel
{
    /// <summary>
    /// New order
    /// </summary>
    public class NewOrder
    {
        /// <summary>
        /// Order identification
        /// </summary>
        public Guid OrderId { get; set; }

        /// <summary>
        /// Store identification
        /// </summary>
        public Guid StoreId { get; set; }

        /// <summary>
        /// Total
        /// </summary>
        public decimal Total { get; set; }

        /// <summary>
        /// Products
        /// </summary>
        public ICollection<NewOrderProduct> Products { get; set; }

        /// <summary>
        /// ProductionIds
        /// </summary>
        public ICollection<Guid> ProductionIds { get; set; }
    }
}
