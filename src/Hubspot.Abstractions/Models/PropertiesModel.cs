namespace Withywoods.Hubspot.Abstractions.Models;

public partial class PropertiesModel
{
    public ValueModel FirstName { get; set; } =  new();

    public ValueModel? LastModifiedDate { get; set; }

    public ValueModel Company { get; set; } = new();

    public ValueModel LastName { get; set; } = new();
}
