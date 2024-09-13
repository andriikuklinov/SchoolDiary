import React, { ReactElement, useState } from "react";
import { BsSortUp } from "react-icons/bs";
import { BsSortDown } from "react-icons/bs";

interface Props {
    children: string,
    propertyName: string,
    onClick: (propertyName: string, orderValue: string) => void
}

export default function THeadBtn({ children, propertyName, onClick }: Props) {
    const [orderBy, setOrderBy] = useState('');
    const handleTHeadBtnOnClick = () => {
        let value = '';
        if (orderBy == '')
            value ='asc';
        else if (orderBy == 'asc')
            value= 'desc';
        else
            value = '';
        setOrderBy(value);
        onClick(propertyName, value);
    }
    const setIconBtn = () => {
        if (orderBy == 'asc')
            return <BsSortUp size={15} />
        else if(orderBy == 'desc')
            return <BsSortDown size={15} />
        return null;
    }
    return <>
        <button onClick={handleTHeadBtnOnClick} className="thead-btn">
            {children}{setIconBtn()}
        </button>
    </>
}