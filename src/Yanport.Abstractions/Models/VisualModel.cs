using System.Collections.Generic;

namespace Devpro.Yanport.Abstractions.Models
{
    public class VisualModel
    {
        public List<string> Images { get; set; }
        public List<object> Orientations { get; set; }
        public List<object> Views { get; set; }
    }
}
