using System.Collections.Generic;

namespace GeekBurger.Production.Contract
{
    public class Restriction
    {
        public bool On { get; set; }
        public ICollection<string> Restrictions { get; set; }
    }
}
