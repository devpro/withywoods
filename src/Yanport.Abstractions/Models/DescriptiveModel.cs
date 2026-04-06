using System.Collections.Generic;

namespace Withywoods.Yanport.Abstractions.Models
{
    public class DescriptiveModel
    {
        public string Description { get; set; } = string.Empty;

        public List<object> Pros { get; set; } = [];

        public List<object> Cons { get; set; } = [];

        public EquipmentsModel Equipments { get; set; } = new();

        public object? Services { get; set; }
    }
}
