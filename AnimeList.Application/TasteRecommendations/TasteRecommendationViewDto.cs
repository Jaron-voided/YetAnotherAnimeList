using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeList.Application.TasteRecommendations;

public class TasteRecommendationViewDto
{
    public int SeedCount { get; set; } // How many anime IDs I passed
    public int SimilarUserCount { get; set; } // How many users have seed anime in their list
    public List<TasteRecommendationItemDto> Recommendations { get; set; } = new();
}
