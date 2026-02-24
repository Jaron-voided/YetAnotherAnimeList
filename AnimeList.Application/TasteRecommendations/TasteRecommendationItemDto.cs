using AnimeList.Application.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeList.Application.TasteRecommendations;

public class TasteRecommendationItemDto : AnimeItemDto
{
/*    public int MalId { get; set; }
    public string Title { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public double Score { get; set; }*/
    public int RecommendationCount { get; set; }
}
