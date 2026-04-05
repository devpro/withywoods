using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Devpro.Yanport.Abstractions.Models
{
    public class MarketingModel
    {
        public float Price { get; set; }
        public List<PriceEventModel> PriceEvents { get; set; }
        public List<DealerModel> Dealers { get; set; }
        public DateTime PublicationStartDate { get; set; }
        public DateTime PublicationEndDate { get; set; }
        public bool Occupied { get; set; }
        public bool ExclusiveMandate { get; set; }
        public float PriceM2 { get; set; }
        public float RelevantPrice { get; set; }
        public bool Active { get; set; }
        public int PublicationDays { get; set; }
        public bool FeesIncluded { get; set; }
        public bool RentalExpensesIncluded { get; set; }
        public float PriceCC { get; set; }
        public float PriceM2CC { get; set; }
        [JsonProperty("type")]
        public string MarketingType { get; set; }
    }
}
