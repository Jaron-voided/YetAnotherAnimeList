using AnimeList.Application.Mapping;
using AnimeList.Domain.Enums;
using AnimeList.Tests.Unit.TestData;

namespace AnimeList.Tests.Unit.MapperTests;

public class AnimeMapperTests
{
    private readonly AnimeMapper _mapper = new();

    [Fact]
    public void MalIdMapperTest()
    {
        // Arrange
        var model = AnimeTestData.Bebop;
      
        // Act
        var result = _mapper.ToDto(model);
      
        // Assert
        Assert.NotNull(result);
        Assert.Equal(model.MalId, result.MalId);
        Assert.NotEqual(model.MalId + 1, result.MalId);
    }
   
    [Fact]
    public void MapRatingTest()
    {
        // Arrange
        var model = AnimeTestData.Bebop;
      
        // Act
        var result = _mapper.ToDto(model);
      
        // Assert
        Assert.NotNull(result);
        Assert.Equal(model.Rating.ToString(), result.Rating);
        Assert.NotEqual(AnimeEnums.AnimeRating.PG.ToString(), result.Rating);
    }
   
    [Fact]
    public void MapStatusTest()
    {
        // Arrange
        var model = AnimeTestData.Bebop;
      
        // Act
        var result = _mapper.ToDto(model);
      
        // Assert
        Assert.NotNull(result);
        Assert.Equal(model.Status.ToString(), result.Status);
        Assert.NotEqual(AnimeEnums.AnimeStatus.CurrentlyAiring.ToString(), result.Status);
    }
   
    [Fact]
    public void MapTypeTest()
    {
        // Arrange
        var model = AnimeTestData.Bebop;
      
        // Act
        var result = _mapper.ToDto(model);
      
        // Assert
        Assert.NotNull(result);
        Assert.Equal(model.Type.ToString(), result.Type);
        Assert.NotEqual(AnimeEnums.AnimeType.Movie.ToString(), result.Type);
    }

    [Fact]
    public void MapStartDateTest()
    {
        // Arrange
        var model = AnimeTestData.Bebop;
        
        // Act
        var result = _mapper.ToDto(model);
        
        // Assert
        Assert.NotNull(result);
        Assert.IsType<DateOnly>(result.StartDate.Value);
        Assert.IsNotType<DateTime>(result.StartDate.Value);
    }
}