import Box from "@mui/material/Box";
import AnimeCard from "../card/AnimeCard.tsx";
import styles from "./AnimeList.module.css"
import {useAnime} from "../../../lib/hooks/useAnime.ts";
import { Typography } from "@mui/material";

export default function AnimeList() {
    const {anime, isPending} = useAnime();

    if (!anime || isPending) return <Typography>Loading...</Typography>;
    return (
        <Box className={styles.grid}>
            {anime.slice(0, 40).map((anime) => (
                <AnimeCard
                    key={anime.malId}
                    anime={anime}
                />
            ))}
        </Box>
    )
}