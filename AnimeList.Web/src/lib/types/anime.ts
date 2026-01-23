export const AnimeTypes = [
    "Movie",
    "Music",
    "OVA",
    "Special",
    "TV",
    "ONA",
    "CM",
    "TVSpecial",
    "PV",
    "Unknown"
] as const;

export type AnimeType = typeof AnimeTypes[number];

export const AnimeStatuses = [
    "FinishedAiring",
    "CurrentlyAiring",
    "NotYetAired",
    "Unknown"
] as const;

export type AnimeStatus = typeof AnimeStatuses[number];

export const AnimeRatings = [
    "G",
    "PG",
    "PG13",
    "R",
    "RPlus",
    "Rx",
    "Unknown"
] as const;

export type AnimeRating = typeof AnimeRatings[number];


export type Anime = {
    malId: number;
    title: string;
    imageUrl: string;
    type: AnimeType;
    status: AnimeStatus;
    rating: AnimeRating;
    score: number | null;
    startDate: string | null;
    endDate: string | null;
    synopsis: string | null;
    rank: number | null;
    popularity: number | null;
    genres: string | null;
    episodes: number | null;
    year: number | null;
    streaming: string | null;
}