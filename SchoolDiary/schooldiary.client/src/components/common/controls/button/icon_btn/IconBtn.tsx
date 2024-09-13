import React, { ReactElement } from "react";

interface Props {
    text?: string,
    className: string
    children?: ReactElement,
    onClick: () => void
}
export default function IconBtn({ text, className, children, onClick }: Props) {
    return <>
        <button onClick={onClick} className={className}>{children}{text}</button>
    </>
}