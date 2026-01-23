import {Card, CardActions, CardContent, CardMedia, Typography} from "@mui/material";
import Button from "@mui/material/Button";
import {useNavigate, useParams} from "react-router";
import {useAnime} from "../../../lib/hooks/useAnime.ts";


export default function AnimeDetail() {
    const navigate = useNavigate();
    const {malId} = useParams<{ malId: string }>();
    const animeId = Number(malId);
    const {singleAnime, isLoadingAnime} = useAnime(animeId);

    if (isLoadingAnime) return <Typography>Loading....</Typography>

    if (!singleAnime) return <Typography>Anime not found...</Typography>;

    return (
        <Card sx={{borderRadius: 3}}>
            <CardMedia
                component='img'
                src={`${singleAnime.imageUrl}`}
                sx={{
                    maxHeight: 420,
                    width: '100%',
                    objectFit: 'contain',
                    backgroundColor: 'rgba(0,0,0,0.04)'
                }}
            />
            <CardContent>
                <Typography variant="h5">{singleAnime.title}</Typography>
                <Typography variant="h5">{singleAnime.rating}</Typography>
                <Typography variant="body1">{singleAnime.synopsis}</Typography>
            </CardContent>
            <CardActions>
                <Button color="primary">View</Button>
                <Button
                    onClick={() => navigate('/anime')}
                    color="inherit"
                >
                    Cancel
                </Button>
            </CardActions>
        </Card>
    )
}