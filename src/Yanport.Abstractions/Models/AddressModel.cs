using System.Collections.Generic;

namespace Withywoods.Yanport.Abstractions.Models;

public class AddressModel
{
    public int CityId { get; set; }

    public List<object> IrisIds { get; set; } = [];

    public List<object> ParcelIds { get; set; } = [];

    public int QuarterId { get; set; }

    public long AgglomerationId { get; set; }

    public string? StreetNumber { get; set; }

    public string Street { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string ZipCode { get; set; } = string.Empty;

    public string DeptCode { get; set; } = string.Empty;

    public string? Formatted { get; set; }

    public string Country { get; set; } = string.Empty;

    public LocationModel? Location { get; set; }

    public object? ApproximateLocation { get; set; }
}
