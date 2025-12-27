import type { Anime, AnimeDto } from '../anime'

const toGenresArray = (genres?: string | null): string[] => {
  if (!genres) return []
  return genres
    .split(',')
    .map((g) => g.trim())
    .filter(Boolean)
}

export const mapAnimeDto = (dto: AnimeDto): Anime => ({
  id: dto.malId,
  title: dto.title,
  imageUrl: dto.imageUrl,
  type: dto.type,
  status: dto.status,
  rating: dto.rating,
  score: dto.score ?? null,
  startDate: dto.startDate ?? null,
  endDate: dto.endDate ?? null,
  synopsis: dto.synopsis ?? null,
  rank: dto.rank ?? null,
  popularity: dto.popularity ?? null,
  genres: toGenresArray(dto.genres),
  episodes: dto.episodes ?? null,
  year: dto.year ?? null,
  streaming: dto.streaming ?? null,
})

export const mapAnimeDtoList = (dtos: AnimeDto[]): Anime[] =>
  dtos.map(mapAnimeDto)
