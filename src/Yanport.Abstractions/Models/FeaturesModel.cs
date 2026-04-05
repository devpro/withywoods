namespace Devpro.Yanport.Abstractions.Models
{
    public class FeaturesModel
    {
        public DescriptiveModel Descriptive { get; set; }
        public VisualModel Visual { get; set; }
        public GeometryModel Geometry { get; set; }
        public ConstructionModel Construction { get; set; }
        public EnergyModel Energy { get; set; }
        public object Condominium { get; set; }
    }
}
