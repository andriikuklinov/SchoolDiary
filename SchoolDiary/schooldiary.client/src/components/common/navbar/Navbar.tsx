import React from "react";

export default function Navbar() {
    return <>
        <nav className="navbar navbar-dark bg-dark justify-content-between">
            <a className="navbar-brand">School Diary</a>
            <form className="search-form">
                <input className="form-control mr-sm-2 search-input" type="search" placeholder="Search" aria-label="Search" />
                <button className="btn btn-outline-success my-2 my-sm-0 search-btn" type="submit">Search</button>
            </form>
        </nav>
    </>
}