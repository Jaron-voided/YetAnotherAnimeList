import './styles.css'
import {Container, CssBaseline} from "@mui/material";
import NavBar from "./NavBar.tsx";
import Box from "@mui/material/Box";
import {Outlet} from "react-router";

function App() {

    return (
        <Box sx={{bgcolor: '#272626'}}>
            <CssBaseline />
            <NavBar/>
            <Container maxWidth='xl' sx={{mt: 3}}>
                <Outlet />
            </Container>
        </Box>
    )
}

export default App
