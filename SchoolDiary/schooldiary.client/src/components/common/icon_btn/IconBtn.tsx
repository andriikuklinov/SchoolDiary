import React from "react";
import { ReactElement } from "react";

interface Props {
    text?: string,
    className?: string,
    disabled?: boolean,
    children: ReactElement
}
export default function IconBtn({ text, children, className, disabled }: Props) {
    return <>
        <button disabled={ disabled } className={ className }>{children}</button>
    </>;
}