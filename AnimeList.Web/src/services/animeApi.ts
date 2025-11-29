import type { AnimeDto } from '../models'
import { mapAnimeDto, mapAnimeDtoList } from '../models/mappers/anime'

const API_BASE = '/api/anime'

async function fetchJson<T>(path: string): Promise<T> {
  const response = await fetch(path)
  if (!response.ok) {
    const message = await response.text().catch(() => response.statusText)
    throw new Error(`Request failed (${response.status}): ${message}`)
  }
  return (await response.json()) as T
}

export async function fetchAnimeDtoList(): Promise<AnimeDto[]> {
  return fetchJson<AnimeDto[]>(API_BASE)
}

export async function fetchAnimeDto(id: number): Promise<AnimeDto> {
  return fetchJson<AnimeDto>(`${API_BASE}/${id}`)
}

export async function fetchAllAnime() {
  const dtos = await fetchAnimeDtoList()
  return mapAnimeDtoList(dtos)
}

export async function fetchAnimeById(id: number) {
  const dto = await fetchAnimeDto(id)
  return mapAnimeDto(dto)
}
