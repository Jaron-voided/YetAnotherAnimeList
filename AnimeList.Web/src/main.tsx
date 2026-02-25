import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './app/layout/styles.css'
//import './index.css'
//import App from './app/layout/App.tsx'
import '@fontsource/roboto/300.css';
import '@fontsource/roboto/400.css';
import '@fontsource/roboto/500.css';
import '@fontsource/roboto/700.css';
//import {BrowserRouter} from "react-router";
import { router } from './app/router/Routes.tsx'
import { RouterProvider } from "react-router-dom";
import { QueryClientProvider, QueryClient } from '@tanstack/react-query';

const queryClient = new QueryClient();

createRoot(document.getElementById('root')!).render(
   <StrictMode>
        <QueryClientProvider client={queryClient}>
            <RouterProvider router={router} />
        </QueryClientProvider> 
{/*    <BrowserRouter>
        <App />
    </BrowserRouter>*/}
  </StrictMode>,
)
