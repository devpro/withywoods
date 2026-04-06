using System.Collections.Generic;

namespace Withywoods.Yanport.Abstractions.Models;

public class ResultModel
{
    public int Total { get; set; }

    public List<HitModel> Hits { get; set; } = [];
}
