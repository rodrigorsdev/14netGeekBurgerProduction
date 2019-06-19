using GeekBurger.Production.Application.ViewModel;
using GeekBurger.Production.Contract;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekBurger.Production.Models
{
    /// <summary>
    /// Order Changed Event
    /// </summary>
    public class OrderChangedEvent
    {
        /// <summary>
        /// Event Identification
        /// </summary>
        [Key]
        public Guid EventId { get; set; }

        /// <summary>
        /// Order state enumerator
        /// </summary>
        public OrderState State { get; set; }

        /// <summary>
        /// Order
        /// </summary>
        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        /// <summary>
        /// Indicates whether the message was sent
        /// </summary>
        public bool MessageSent { get; set; }
    }
}
