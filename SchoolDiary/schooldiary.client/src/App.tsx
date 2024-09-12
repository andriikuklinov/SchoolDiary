import React, { useState } from 'react'
import './App.css'
import { Outlet } from 'react-router-dom'
import Sidebar from './components/common/sidebar/Sidebar'
import Navbar from './components/common/navbar/Navbar'

export default function App() {
    const [mainClass, setMainClass] = useState('collapsed');

    const toogleMainLeftMargin = (isSidebarHidden: boolean) => {
        setMainClass(isSidebarHidden ? "stretched" : "collapsed");
    }


    return (
        <>
            <Sidebar toogleMainLeftMargin={toogleMainLeftMargin} />
            <main className={mainClass}>
                <Navbar />
                <div className="container">
                    <Outlet />
                </div>
            </main>
        </>
    )
}
