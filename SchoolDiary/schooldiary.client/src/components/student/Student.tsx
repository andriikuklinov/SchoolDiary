import React, { useEffect, useState } from "react";
import Table from "../common/data_table/Table";
import ModelBase from "../common/model/ModelBase";
import { StudentService } from "../../api/StudentService";
import { serverUrl } from "../../api/Api.module";
import ColumnModel from "../common/model/ColumnModel";
import buildOrderObject from "../common/Common";
import buildOrderBySettings from "../common/Common";
import Pagination from "../common/pagination/Pagination";

interface Student extends ModelBase {
    firstName: string
    lastName: string,
    middleName: string,
    email: string,
    phoneNumber: string,
    address: string,
    birthDate: Date,
    enrolnmentDate: Date,
    groupId: number
}
export default function Student() {
    const columns: ColumnModel[] = [
        { header: 'Id', propertyName: 'id' },
        { header: 'First Name', propertyName: 'firstName' },
        { header: 'Last Name', propertyName: 'lastName' },
        { header: 'Middle Name', propertyName: 'middleName' },
        { header: 'Email', propertyName: 'email' },
        { header: 'Phone Number', propertyName: 'phoneNumber' },
        { header: 'Address', propertyName: 'address' },
        { header: 'Birth Date', propertyName: 'birthDate' },
        { header: 'Enrolnment Date', propertyName: 'enrolnmentDate' },
        { header: 'Group Id', propertyName: 'groupId' }
    ]
    const studentService: StudentService = new StudentService(serverUrl);
    const [data, setData] = useState<Student[]>([]);
    const [orderSettings, setOrderSettings] = useState<Array<{ "PropertyName": string, "Direction": string }>>([]);
    
    useEffect(() => {
        debugger;
        let orderSettingsString = orderSettings.length > 0 ? `{"data":${JSON.stringify(orderSettings)}}` : '';
        studentService.getStudents('', orderSettingsString).then((response) => {
            if (response.data.isSuccess) {
                setData(response.data.result as Student[]);
            }
        });
    }, [orderSettings]);

    const edit = (student: ModelBase) => {
        let res = student as Student;
        console.log(student);
    }
    const remove = (student: ModelBase) => {
        studentService.removeStudent(student.id).then((response) => {
            if (response.data.isSuccess) {
                setData((prevState) => { return prevState.filter((element) => { return element.id != response.data.result.id }) });
            }
        });
    }
    const orderBy = (propertyName: string, direction: string) => {
        buildOrderBySettings(orderSettings, setOrderSettings, propertyName, direction);
    }
    return <>
        <Table
            columns={columns}
            data={data}
            orderBy={orderBy}
            onEdit={edit}
            onRemove={remove}
        />
        <Pagination />
    </>
}