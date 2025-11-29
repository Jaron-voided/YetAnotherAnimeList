using System;
using AnimeList.Domain.Enums;
using AnimeList.Domain.Models;
using AnimeList.Persistence.CSV;
using AnimeList.Tests.Unit.TestData;
using Xunit;

namespace AnimeList.Tests.Unit.MapperTests;

public class RawAnimeMapperTests
{
   private readonly RawAnimeMapper _mapper = new();

   [Fact]
   public void MalIdMapperTest()
   {
      // Arrange
      var raw = RawAnimeTestData.Bebop;
      
      // Act
      var result = _mapper.Map(raw);
      
      // Assert
      Assert.NotNull(result);
      Assert.Equal(raw.MalId, result.MalId);
      Assert.NotEqual(raw.MalId, result.MalId + 1);
   }
   
   [Fact]
   public void MapRatingTest()
   {
      // Arrange
      var raw = RawAnimeTestData.Bebop;
      
      // Act
      var result = _mapper.Map(raw);
      
      // Assert
      Assert.NotNull(result);
      Assert.Equal(AnimeEnums.AnimeRating.PG13, result.Rating);
      Assert.NotEqual(AnimeEnums.AnimeRating.PG, result.Rating);
   }
   
   [Fact]
   public void MapStatusTest()
   {
      // Arrange
      var raw = RawAnimeTestData.Bebop;
      
      // Act
      var result = _mapper.Map(raw);
      
      // Assert
      Assert.NotNull(result);
      Assert.Equal(AnimeEnums.AnimeStatus.FinishedAiring, result.Status);
      Assert.NotEqual(AnimeEnums.AnimeStatus.CurrentlyAiring, result.Status);
   }
   
   [Fact]
   public void MapTypeTest()
   {
      // Arrange
      var raw = RawAnimeTestData.Bebop;
      
      // Act
      var result = _mapper.Map(raw);
      
      // Assert
      Assert.NotNull(result);
      Assert.Equal(AnimeEnums.AnimeType.TV, result.Type);
      Assert.NotEqual(AnimeEnums.AnimeType.Movie, result.Type);
   }
}
