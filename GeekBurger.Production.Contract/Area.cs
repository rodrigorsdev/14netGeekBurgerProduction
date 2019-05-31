using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GeekBurger.Production.Contract
{
    public class Area
    {
        [JsonProperty(PropertyName = "id")]
        public Guid AreaId { get; set; }
        public string AreaDescription { get; set; }
        public bool On { get; set; }
        public ICollection<string> Restrictions { get; set; }
    }
}
