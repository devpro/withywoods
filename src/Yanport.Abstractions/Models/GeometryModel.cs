using System.Collections.Generic;

namespace Devpro.Yanport.Abstractions.Models
{
    public class GeometryModel
    {
        public float Surface { get; set; }
        public int RoomCount { get; set; }
        public List<AreaModel> Areas { get; set; }
        public List<object> Floors { get; set; }
        public AreaCountModel AreaCount { get; set; }
    }
}
