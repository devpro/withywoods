using System.Collections.Generic;

namespace Devpro.Yanport.Abstractions.Models
{
    public class DescriptiveModel
    {
        public string Description { get; set; }
        public List<object> Pros { get; set; }
        public List<object> Cons { get; set; }
        public EquipmentsModel Equipments { get; set; }
        public object Services { get; set; }
    }
}
