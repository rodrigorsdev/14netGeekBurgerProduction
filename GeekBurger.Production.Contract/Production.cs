using System;

namespace GeekBurger.Production.Contract
{
    public class Production
    {
        public Guid TransactionId { get; set; }
        public Guid ProductionId { get; set; }
        public Area Restrictions { get; set; }
    }
}