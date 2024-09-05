import React, { useState } from 'react';
import AuthLayout from '../auth_layout/AuthLayout';
import { FieldValues, useForm } from 'react-hook-form';
import { z } from 'zod';
import { zodResolver } from '@hookform/resolvers/zod';
import { AuthService } from '../../api/AuthService';
import { serverUrl } from '../../api/Api.module';

const schema = z.object({
    email: z.string().nonempty("Email is required").email(),
    password: z.string().nonempty("Password is required")
});

type FormData = z.infer<typeof schema>;

export default function Login() {
    const [ errorMessage, setErrorMessage ] = useState('');
    const { register, handleSubmit, formState: { errors, isValid } } = useForm<FormData>({ resolver: zodResolver(schema) });
    const onSubmit = (data: FieldValues) => {
        const authService: AuthService = new AuthService(serverUrl);
        authService.login(data.email, data.password).then((response) => {
            setErrorMessage('');
            if (response.data.isSuccess) {
                localStorage.setItem('jwt', response.data.result);
            }
            else {
                setErrorMessage(response.data.errorMessage);
            }
        });
    }
    return (
        <>
            <AuthLayout header="Sign In">
                <form className="login-form" onSubmit={handleSubmit(onSubmit)}>
                    <div className="mb-3">
                        <input {...register('email')} type="email" placeholder="Email" className="form-control" />
                        {errors.email && <p className="text-danger">{errors.email.message}</p>}
                    </div>
                    <div className="mb-3">
                        <input {...register('password')} type="password" placeholder="Password" className="form-control" />
                        {errors.password && <p className="text-danger">{errors.password.message}</p>}
                    </div>
                    <p className="text-danger">{errorMessage}</p>
                    <div className="justify-content-between">
                        <span>
                            <input type="checkbox" className="form-check-input" />
                            <label className="form-check-label">Remember me</label>
                        </span>
                        <a className="forgot-pass-link">Forgot password?</a>
                    </div>
                    <div className="login-form-actions">
                        <button disabled={ !isValid } className="btn btn-primary btn-lg login-btn">LOGIN</button>
                        <p style={{ clear: 'both', paddingTop: '10px', float: 'left' }} className="small">Don`t have an account? &nbsp;
                            <a className="link-danger" href="#">Register</a>
                        </p>
                    </div>
                </form>
            </AuthLayout>
        </>
    );
}