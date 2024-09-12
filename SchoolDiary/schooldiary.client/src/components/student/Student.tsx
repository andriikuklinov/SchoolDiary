import React from "react";
import Table from "../common/data_table/Table";

export default function Student() {
    return <>
        <Table columns={['Id', 'Name', 'Phone Number']} data={[{ id: '1', name: 'Andrii', phoneNumber: '+380635654356' }, { id: '1', name: 'Andrii', phoneNumber: '+380635654356' }] } />
    </>
}