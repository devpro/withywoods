using System.Collections.Generic;

namespace Devpro.Yanport.Abstractions.Models
{
    public class EnergyModel
    {
        public List<string> HeatingTypes { get; set; }
        public List<object> WaterHeatingTypes { get; set; }
        public string HeatingMode { get; set; }
    }
}
