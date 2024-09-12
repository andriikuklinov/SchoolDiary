import React from "react";

interface Props {
    columns: string[],
    data: object[]
}
export default function Table(props: Props) {
    return <>
        <table className="table">
            <thead>
                <tr>
                    {props.columns.map((column => {
                        return <th>{ column }</th>
                    }))}
                </tr>
            </thead>
            <tbody>
                {props.data.map((obj: {}) => {
                    return <tr>{ Object.keys(obj).map((key: string) => { return <td>{ obj[key as keyof typeof obj] }</td> })}</tr>
                })}
            </tbody>
            <tfoot></tfoot>
        </table>
    </>
}