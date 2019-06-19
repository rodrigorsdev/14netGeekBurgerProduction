using System;
using System.Collections.Generic;

namespace GeekBurger.Production.Contract
{
    /// <summary>
    /// Production
    /// </summary>
    public class Production
    {
        /// <summary>
        /// Production id
        /// </summary>
        public Guid ProductionId { get; set; }

        /// <summary>
        /// List of restrictions
        /// </summary>
        public ICollection<string> Restrictions { get; set; }

        /// <summary>
        /// Indicates whether the production is on or off
        /// </summary>
        public bool On { get; set; }
    }
}