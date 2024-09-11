import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import App from './App'
import './index.css'
import React from 'react'
import 'bootstrap/dist/css/bootstrap.min.css'
import { createBrowserRouter, RouterProvider } from 'react-router-dom'
import Login from './components/login/Login'
import Register from './components/register/Register'
import ForgotPassword from './components/forgot_password/ForgotPassword'
import ForgotEmail from './components/forgot_email/ForgotEmail'
import Student from './components/student/Student'

const router = createBrowserRouter([
    {
        path: "/",
        element: <App />,
        children: [
            {
                path: "student",
                element: <Student />
            }
        ]
    },
    {
        path: "login",
        element: <Login />
    },
    {
        path: "register",
        element: <Register />
    },
    {
        path: "forgot-email",
        element: <ForgotEmail />
    },
    {
        path: "forgot-password",
        element: <ForgotPassword />
    }
]);

createRoot(document.getElementById('root')!).render(
    <StrictMode>
        <RouterProvider router={router} />
    </StrictMode>
)
