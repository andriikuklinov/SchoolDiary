import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import App from './App'
import './index.css'
import React from 'react'
import 'bootstrap/dist/css/bootstrap.min.css'
import { createBrowserRouter, RouterProvider } from 'react-router-dom'
import Login from './components/login/Login'

const router = createBrowserRouter([
    {
        path: "/",
        element: <App />,
        children: []
    },
    {
        path: "login",
        element: <Login />
    }
]);

createRoot(document.getElementById('root')!).render(
    <StrictMode>
        <RouterProvider router={router} />
    </StrictMode>
)
