using System.Diagnostics;
using System.Globalization;
using AnimeList.Application.RepoInterfaces.Anime;
using CsvHelper;

namespace AnimeList.Persistence.CSV.AnimeRatings;

public class AnimeRatingsFilter
{
    private readonly IAnimeRepository _animeRepository;

    public AnimeRatingsFilter(IAnimeRepository animeRepository)
    {
        _animeRepository = animeRepository;
    }

    internal IEnumerable<RawAnimeRatingsDto> Filter(IEnumerable<RawAnimeRatingsDto> animeRatings, HashSet<int> validIds)
    {
        var sw = new Stopwatch();
        sw.Start();
        int count = 0;

        // This needs called at a different layer...
        /*var idsTask = _animeRepository.GetAllMalIdsAsync();
        HashSet<int> validIds = await idsTask;*/

        foreach (var animeRatingDto in animeRatings)
        {
            if (IsUsernameValid(animeRatingDto) && IsValidId(validIds, animeRatingDto))
            {
                count++;
                yield return animeRatingDto;
            }
        }
        sw.Stop();
        Console.WriteLine("Parsed rows = "  + count);
        Console.WriteLine($"Elapsed time for filtering: {sw.Elapsed}");
    }

    private bool IsValidId(HashSet<int> validIds, RawAnimeRatingsDto animeRatingDto)
    {
        if (validIds.Contains(animeRatingDto.MalId))
            return true;

        return false;
    }

    private bool IsUsernameValid(RawAnimeRatingsDto animeRatingDto)
    {
        if (animeRatingDto.Username == null)
            return false;
        string name = animeRatingDto.Username.Trim();
        if (string.IsNullOrEmpty(name))
            return false;
        
        return true;
    }
}