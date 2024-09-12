import React from "react";
import { BsFillPencilFill } from "react-icons/bs";
import { BsFillTrash3Fill } from "react-icons/bs";
import ModelBase from "../model/ModelBase";
import IconBtn from "../icon_btn/IconBtn";

interface Props {
    columns: string[],
    data: ModelBase[],
    remove: (model: ModelBase) => void,
    edit: (model: ModelBase) => void
}
export default function Table(props: Props) {
    return <>
        <table className="table">
            <thead>
                <tr>
                    {props.columns.map((column => {
                        return <th key={ column }>{ column }</th>
                    }))}
                </tr>
            </thead>
            <tbody>
                {props.data.map((obj: ModelBase, index) => {
                    return <tr key={index}>{
                        Object.keys(obj).map((key: string) => {
                            return <td key={key}>{obj[key as keyof typeof obj]}</td>
                        })}
                        <td>
                            <IconBtn className="icon-btn">
                                <BsFillPencilFill size={15} onClick={() => { props.edit(obj) }} />
                            </IconBtn>
                            <IconBtn className="icon-btn">
                                <BsFillTrash3Fill color="red" size={15} onClick={() => { props.remove(obj) }} />
                            </IconBtn>
                        </td>
                    </tr>
                })}
            </tbody>
            <tfoot></tfoot>
        </table>
    </>
}