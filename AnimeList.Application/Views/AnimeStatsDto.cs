using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeList.Application.Views;

public class AnimeStatsDto
{
    public int Watching { get; set; }
    public int Completed { get; set; }
    public int OnHold { get; set; }
    public int Dropped { get; set; }
    public int PlanToWatch { get; set; }
    public int Total { get; set; }
    public double? Score8Votes { get; set; }
    public double? Score9Votes { get; set; }
    public double? Score10Votes { get; set; }
}
