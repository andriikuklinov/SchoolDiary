import React from "react";
import Table from "../common/data_table/Table";
import ModelBase from "../common/model/ModelBase";

interface Student extends ModelBase {
    
}

export default function Student() {
    const edit = (student: Student) => {
        console.log(student);
    }
    const remove = (student: Student) => {
        console.log(student);
    }

    return <>
        <Table edit={edit} remove={ remove } columns={['Id', 'Name', 'Phone Number']} data={[{ id: '1', name: 'Andrii', phoneNumber: '+380635654356' }, { id: '1', name: 'Andrii', phoneNumber: '+380635654356' }] } />
    </>
}