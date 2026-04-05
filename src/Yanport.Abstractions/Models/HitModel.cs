using System.Collections.Generic;
using Newtonsoft.Json;

namespace Devpro.Yanport.Abstractions.Models
{
    public class HitModel
    {
        public string Id { get; set; }
        public string Source { get; set; }
        public MainFeaturesModel Features { get; set; }
        public MarketingModel Marketing { get; set; }
        public AddressModel Address { get; set; }
        public List<AdModel> Ads { get; set; }
        public List<object> Properties { get; set; }
        [JsonProperty("type")]
        public string HitType { get; set; }
    }
}
