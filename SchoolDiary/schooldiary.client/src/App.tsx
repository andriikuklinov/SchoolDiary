import React from 'react'
import './App.css'
import { Outlet } from 'react-router-dom'

export default function App() {

    return (
        <>
            <p>Sidebar</p>
            <Outlet />
        </>
    )
}
