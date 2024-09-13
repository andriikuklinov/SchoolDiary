import React from "react";
import IconBtn from "../controls/button/icon_btn/IconBtn";
import { BsFillPencilFill } from "react-icons/bs";
import { BsFillTrashFill } from "react-icons/bs";
import ModelBase from "../model/ModelBase";
import THeadBtn from "../controls/button/thead_btn/TheadBtn";
import ColumnModel from "../model/ColumnModel";
interface Props {
    columns: ColumnModel[],
    data: ModelBase[],
    onEdit: (model: ModelBase) => void,
    onRemove: (model: ModelBase) => void,
    orderBy: (prop: string, value: string) => void
}
export default function Table(props: Props) {
    return <>
        <table className="table">
            <thead>
                <tr>
                    {props.columns.map((column => {
                        return <th key={column.header}>
                            <THeadBtn propertyName={column.propertyName} onClick={props.orderBy}>{column.header}</THeadBtn>
                        </th>
                    }))}
                </tr>
            </thead>
            <tbody>
                {props.data.map((obj: ModelBase) => {
                    return <tr key={obj.id}>
                        {Object.keys(obj).map((key: string) => { return <td key={key}>{obj[key as keyof typeof obj]}</td> })}
                        <td key={ "operation" + obj.id}>
                            <IconBtn onClick={() => { props.onEdit(obj)}} className="icon-btn">
                                <BsFillPencilFill size={15} />
                            </IconBtn>
                            <IconBtn onClick={() => { props.onRemove(obj)} } className="icon-btn">
                                <BsFillTrashFill size={15} color="red" />
                            </IconBtn>
                        </td>
                    </tr>
                })}
            </tbody>
            <tfoot></tfoot>
        </table>
    </>
}