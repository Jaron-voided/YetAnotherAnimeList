using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeList.Application.Views;

public class AnimeRecommendationsViewDto
{
    public int MalId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public double? Score { get; set; }
    public List<AnimeItemDto> Recommendations { get; set; } = new();
}
