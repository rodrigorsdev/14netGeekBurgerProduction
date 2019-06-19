using System;

namespace GeekBurger.Production.Application.ViewModel
{
    /// <summary>
    /// Update Order
    /// </summary>
    public class UpdateOrder
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
        /// Order state enumerator
        /// </summary>
        public OrderState State { get; set; }
    }
}
