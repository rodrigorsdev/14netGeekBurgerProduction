using GeekBurger.Production.Application.ViewModel;
using GeekBurger.Production.Contract;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekBurger.Production.Models
{
    public class OrderChangedEvent
    {
        [Key]
        public Guid EventId { get; set; }

        public OrderState State { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        public bool MessageSent { get; set; }
    }
}
