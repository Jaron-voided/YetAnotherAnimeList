export interface AnimeDto {
  malId: number
  title: string
  imageUrl: string
  type: string
  status: string
  rating: string
  score: number | null
  startDate: string | null
  endDate: string | null
  synopsis: string | null
  rank: number | null
  popularity: number | null
  genres: string | null
  episodes: number | null
  year: number | null
  streaming: string | null
}

export interface Anime {
  id: number
  title: string
  imageUrl: string
  type: string
  status: string
  rating: string
  score: number | null
  startDate: string | null
  endDate: string | null
  synopsis: string | null
  rank: number | null
  popularity: number | null
  genres: string[]
  episodes: number | null
  year: number | null
  streaming: string | null
}
