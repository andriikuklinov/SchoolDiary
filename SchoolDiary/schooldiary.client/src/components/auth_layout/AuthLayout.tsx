import React from "react";
import { ReactElement } from "react";
import loginFormImg from './../../assets/login_page_logo.png';

interface Props {
    header: string,
    children: ReactElement
}
export default function AuthLayout(props: Props) {
    return <>
        <div className="container login-container">
            <div className="row login-wrapper">
                <div className="col vertical-centered-block">
                    <img className="img-fluid" src={loginFormImg} />
                </div>
                <div className="col vertical-centered-block">
                    <h3 className="text-center">{ props.header }</h3>
                    <hr className="divider"></hr>
                    <center>
                        {props.children}
                    </center>
                </div>
            </div>
        </div>
        <div className="container-fluid">
            <div className="bg-primary login-footer"></div>
        </div>
    </>
}