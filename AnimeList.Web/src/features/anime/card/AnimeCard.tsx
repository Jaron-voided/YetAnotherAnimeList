import type {Anime} from "../../../lib/types/anime.ts";
import {Card, CardActions, CardContent, CardMedia, Chip, Typography} from "@mui/material";
import MenuItemLink from "../../../app/shared/MenuItemLink.tsx";

type Props = {
    anime: Anime
}

export default function AnimeCard({anime} : Props) {
    //const navigate = useNavigate();
    return(
        <Card sx={{borderRadius: 3}}>
            <CardContent>
                <CardMedia
                    component='img'
                    src={`${anime.imageUrl}`}
                />
                <Typography variant="h5">{anime.title}</Typography>
                <Typography sx={{color: 'text-secondary', mb: 1}}>{anime.score}</Typography>
                <Typography variant="body2">{anime.startDate}</Typography>
            </CardContent>
            <CardActions sx={{display: 'flex', justifyContent: 'space-between', pb: 2}}>
                <Chip label={anime.type} variant="outlined"/>
                <MenuItemLink to={`/anime/${anime.malId}`}>
                    View
                </MenuItemLink>
            </CardActions>
        </Card>
    )
}