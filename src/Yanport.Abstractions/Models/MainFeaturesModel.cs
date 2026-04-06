using System.Collections.Generic;

namespace Withywoods.Yanport.Abstractions.Models;

public class MainFeaturesModel
{
    public DescriptiveModel Descriptive { get; set; } = new();

    public VisualModel Visual { get; set; } = new();

    public GeometryModel Geometry { get; set; } = new();

    public ConstructionModel Construction { get; set; } = new();

    public EnergyModel Energy { get; set; } = new();

    public object? Condominium { get; set; }

    public List<AdditionalFeatureModel> AdditionalFeatures { get; set; } = [];
}
