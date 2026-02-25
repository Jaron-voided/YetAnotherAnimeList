import {createBrowserRouter} from "react-router-dom";
import App from "../layout/App.tsx";
import HomePage from "../../features/home/HomePage.tsx";
import AnimeDashboard from "../../features/anime/AnimeDashboard.tsx";
import AnimeDetail from "../../features/anime/details/AnimeDetail.tsx";

export const router = createBrowserRouter([
    {
        path: '/',
        element: <App />,
        children: [
            {path: '', element: <HomePage />},
            {path: 'anime', element: <AnimeDashboard />},
            {path: 'anime/:malId', element: <AnimeDetail/>},
/*            {path: 'byType', element: <AnimeType  />},
            {path: 'byRating', element: <AnimeRating  />}*/
        ]
    }
])