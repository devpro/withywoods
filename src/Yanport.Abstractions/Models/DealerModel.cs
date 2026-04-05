using Newtonsoft.Json;

namespace Devpro.Yanport.Abstractions.Models
{
    public class DealerModel
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public long Id { get; set; }
        public string SubType { get; set; }
        public BloctelModel Bloctel { get; set; }
        public bool AgenciesUnwanted { get; set; }
        [JsonProperty("type")]
        public string DealerType { get; set; }
    }
}
