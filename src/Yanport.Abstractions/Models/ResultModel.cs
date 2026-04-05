using System.Collections.Generic;

namespace Devpro.Yanport.Abstractions.Models
{
    public class ResultModel
    {
        public int Total { get; set; }
        public List<HitModel> Hits { get; set; }
    }
}
