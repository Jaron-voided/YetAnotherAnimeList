import {Grid} from "@mui/material";
import AnimeList from "./list/AnimeList.tsx";

export default function AnimeDashboard() {


    return (
        <Grid container>
            {/*switch to 7 if adding filters below...*/}
            <Grid size={12}>
                <AnimeList/>
            </Grid>
{/*            <Grid size={5}>
                Anime Filters go here
            </Grid>*/}
        </Grid>
    )
}