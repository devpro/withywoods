using System.Collections.Generic;

namespace Devpro.Yanport.Abstractions.Models
{


    public class ConstructionModel
    {
        public bool NewBuild { get; set; }
        public List<object> UrbanismRules { get; set; }
        public int Year { get; set; }
    }
}
