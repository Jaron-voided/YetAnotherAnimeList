import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import Button from '@mui/material/Button';
import {Container, MenuItem} from "@mui/material";
import {Group} from "@mui/icons-material";
import {NavLink} from "react-router";
import MenuItemLink from "../shared/MenuItemLink.tsx";

export default function NavBar() {
    return (
        <Box sx={{ flexGrow: 1 }}>
            <AppBar position="static" sx={{
                backgroundImage: 'linear-gradient(135deg, #00E5FF 0%, #7B2FFF 50%, #FF2FB2 100%)'
            }}>
                <Container maxWidth="xl">
                    <Toolbar sx={{display: 'flex', justifyContent:'space-between'}}>
                        <Box>
                            <MenuItem component={NavLink} to='/' sx={{display: 'flex', gap: 2}}>
                                <Group fontSize="large" />
                                <Typography variant="h4" fontWeight="bold">
                                    Yet Another Anime List
                                </Typography>
                            </MenuItem>
                        </Box>
                        <Box sx={{display: 'flex', justifyContent:'space-between'}}>
                            <MenuItemLink to='/anime'>
                                AllAnime
                            </MenuItemLink>

                {/*            <MenuItemLink to='/byType'>
                                ByType
                            </MenuItemLink>

                            <MenuItemLink to='/byRating'>
                                ByRating
                            </MenuItemLink>*/}
                        </Box>
                        <Button size="large" variant="contained" color="warning">Add Anime??</Button>
                    </Toolbar>
                </Container>
            </AppBar>
        </Box>
    );
}
