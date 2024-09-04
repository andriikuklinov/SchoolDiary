import React from 'react';
import loginFormImg from './../../assets/login_page_logo.png';
import { AuthService } from '../../api/AuthService';
import AuthLayout from '../auth_layout/AuthLayout';

export default function Login() {
    //let authService = new AuthService('');
    //let response = authService.login('', '');

    return (
        <>
            <AuthLayout header="Sign In">
                <form className="login-form">
                    <div className="mb-3">
                        <input type="email" placeholder="Email" className="form-control" />
                    </div>
                    <div className="mb-3">
                        <input type="password" placeholder="Password" className="form-control" />
                    </div>
                    <div className="justify-content-between">
                        <span>
                            <input type="checkbox" className="form-check-input" />
                            <label className="form-check-label">Remember me</label>
                        </span>
                        <a className="forgot-pass-link">Forgot password?</a>
                    </div>
                    <div className="login-form-actions">
                        <button className="btn btn-primary btn-lg login-btn">LOGIN</button>
                        <p style={{ clear: 'both', paddingTop: '10px', float: 'left' }} className="small">Don`t have an account? &nbsp;
                            <a className="link-danger" href="#">Register</a>
                        </p>
                    </div>
                </form>
            </AuthLayout>
        </>
    );
}