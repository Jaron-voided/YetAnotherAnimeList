using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeList.Application.TasteRecommendations;

public class TasteRecommendationRequestDto
{
    public IEnumerable<int> MalIds { get; set; } = null!;
    public int MinScore { get; set; } = 7;
    public int MinCommonAnime { get; set; } = 4 ;
    public int MaxRecommendations { get; set; } = 10;
}
