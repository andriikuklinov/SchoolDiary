import React from "react";
import Table from "../common/data_table/Table";
import ModelBase from "../common/model/ModelBase";

interface Student extends ModelBase {
    name: string,
    phoneNumber: string
}
export default function Student() {
    const edit = (student: ModelBase) => {
        let res = student as Student;
        console.log(student);
    }
    const remove = (student: ModelBase) => {
        let res = student as Student;
        console.log(student);
    }

    const data = [{ id: '1', name: 'Andrii', phoneNumber: '+380635654356' }, { id: '2', name: 'Andrii', phoneNumber: '+380635654356' }];

    return <>
        <Table
            columns={['Id', 'Name', 'Phone Number']}
            data={data}
            onEdit={edit}
            onRemove={remove}
        />
    </>
}