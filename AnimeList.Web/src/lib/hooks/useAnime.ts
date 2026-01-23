import {useQuery} from "@tanstack/react-query";
import type {Anime} from "../types/anime.ts";
import agent from "../api/agent.ts";

export const useAnime= (id?: number) => {

    const {data: anime, isPending} = useQuery({
        queryKey: ['anime'],
        queryFn: async () => {
            const response = await agent.get<Anime[]>(`/anime`);
            return response.data;
        }
    });

    const {data: singleAnime, isLoading: isLoadingAnime} = useQuery({
        queryKey: ['anime', id],
        queryFn: async () => {
            const response = await agent.get<Anime>(`/anime/${id}`);
            return response.data;
        },
        enabled: !!id
    })

    return {
        anime,
        isPending,
        singleAnime,
        isLoadingAnime
    }
}