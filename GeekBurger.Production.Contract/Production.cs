using System;
using System.Collections.Generic;

namespace GeekBurger.Production.Contract
{
    public class Production
    {
        public Guid ProductionId { get; set; }
        public ICollection<string> Restrictions { get; set; }
        public bool On { get; set; }
    }
}