import React, { useState } from "react";
import { NavLink } from "react-router-dom";
import { BsPersonCheckFill } from "react-icons/bs";
import { BsPersonCircle } from "react-icons/bs";
import { BsGearFill } from "react-icons/bs";
import { BsFillPeopleFill } from "react-icons/bs";
import { BsFillPersonLinesFill } from "react-icons/bs";
import { BsCalendar2Check } from "react-icons/bs";
import { BsJournalBookmarkFill } from "react-icons/bs";
import { BsFillLayersFill } from "react-icons/bs";

interface Props {
    toogleMainLeftMargin: (isSidebarHidden: boolean) => void
}
export default function Sidebar(props: Props) {
    const [isSidebarHidden, setIsSidebarHidden] = useState(false);
    const toggleSidebar = () => {
        setIsSidebarHidden((prevState) => {
            props.toogleMainLeftMargin(!prevState);
            return !prevState;
        });
    }

    return <>
        <aside className={isSidebarHidden ? "sidebar sidebar-hidden" : "sidebar"}>
            <div onClick={toggleSidebar} className="sidebar-hide-btn">
                <i className={isSidebarHidden ? "arrow right" : "arrow left"}></i>
            </div>
            <div className="sidebar-header">
                <BsPersonCircle size={isSidebarHidden ? 30 : 50} />
                <span className={isSidebarHidden ? "hidden" : ""}><h4 className="sidebar-header-user-info">Andrii Kuklinov</h4></span>
            </div>
            <hr className="sidebar-divider"></hr>
            <ul className="nav nav-pills">
                <li className="nav-item">
                    <div className="menu-item">
                        <BsFillPeopleFill size={30} />
                        <NavLink to="/student" className={isSidebarHidden ? "hidden" : "nav-link"}>Student</NavLink>
                    </div>
                </li>
                <li className="nav-item">
                    <div className="menu-item">
                        <BsFillPersonLinesFill size={30} />
                        <NavLink to="/student" className={isSidebarHidden ? "hidden" : "nav-link"}>Teacher</NavLink>
                    </div>
                </li>
                <li className="nav-item">
                    <div className="menu-item">
                        <BsCalendar2Check size={30} />
                        <NavLink to="/student" className={isSidebarHidden ? "hidden" : "nav-link"}>Course</NavLink>
                    </div>
                </li>
                <li className="nav-item">
                    <div className="menu-item">
                        <BsJournalBookmarkFill size={30} />
                        <NavLink to="/student" className={isSidebarHidden ? "hidden" : "nav-link"}>Assesment</NavLink>
                    </div>
                </li>
                <li className="nav-item">
                    <div className="menu-item">
                        <BsFillLayersFill size={30} />
                        <NavLink to="/student" className={isSidebarHidden ? "hidden" : "nav-link"}>Term</NavLink>
                    </div>
                </li>
            </ul>
            <div className="sidebar-footer">
                <ul className="nav nav-pills">
                    <div className="nav-item">
                        <div className="menu-item">
                            <BsGearFill size={30} />
                            <NavLink to="/student" className="nav-link">Settings</NavLink>
                        </div>
                    </div>
                </ul>
            </div>
        </aside>
    </>
}