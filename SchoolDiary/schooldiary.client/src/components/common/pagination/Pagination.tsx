import React from "react";
import { BsChevronLeft } from "react-icons/bs";
import { BsChevronRight } from "react-icons/bs";

interface Props {
    totalPages: number,
    page: number,
    maxDisplaydPages: number,
    pageCountArray: number[],
    onPageChange: (pageSettings: { page: number, pageSize: number }) => void
}
export default function Pagination({ page, maxDisplaydPages, onPageChange, pageCountArray }: Props) {
    const pageNumbers = Array.from({ length: maxDisplaydPages }, (_, i) => i);
    
    return <>
        <ul className="pagination justify-content-end">
            <li className={page <= 0 ? "page-item disabled" : "page-item"}>
                <button onClick={() => onPageChange({ page: page - 1, pageSize: 3 })} className="page-link">
                    <BsChevronLeft />
                </button>
            </li>
            {
                pageNumbers.map((pageIndex) => <li key={pageIndex} className={page == pageIndex ? "page-item active" : "page-item" }>
                    <button onClick={() => onPageChange({ page: pageIndex, pageSize: 3 })} className="page-link" >{pageIndex + 1}</button>
                </li>)
            }
            <li className={page >= maxDisplaydPages - 1 ? "page-item disabled" : "page-item"}>
                <button onClick={() => onPageChange({ page: page + 1, pageSize: 3 })} className="page-link">
                    <BsChevronRight />
                </button>
            </li>
            <li>
                <select className="page-size" onChange={(e) => onPageChange({ page: page, pageSize: parseInt(e.target.value) })}>
                    {pageCountArray.map(element => <option value={element}>{element}</option>)}
                </select>
            </li>
        </ul>
    </>
}