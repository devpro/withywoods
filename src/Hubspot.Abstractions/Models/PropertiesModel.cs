namespace Withywoods.Hubspot.Abstractions.Models;

public partial class PropertiesModel
{
    public required ValueModel FirstName { get; set; }

    public ValueModel? LastModifiedDate { get; set; }

    public required ValueModel Company { get; set; }

    public required ValueModel LastName { get; set; }
}
