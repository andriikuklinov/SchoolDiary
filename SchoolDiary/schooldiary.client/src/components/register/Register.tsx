import React, { useState } from "react";
import AuthLayout from "../auth_layout/AuthLayout";
import { z } from 'zod';
import { zodResolver } from '@hookform/resolvers/zod';
import { FieldValues, useForm } from "react-hook-form";
import { AuthService } from "../../api/AuthService";
import { serverUrl } from "../../api/Api.module";

const schema = z.object({
    email: z.string().email().nonempty(),
    password: z.string().nonempty(),
    confirmPassword: z.string().nonempty()
}).superRefine(({ email, password, confirmPassword }, ctx) => {
    if (password !== confirmPassword) {
        ctx.addIssue({
            code: "custom",
            message: "The confirmation of password doesn`t match",
            path: ["confirmPassword"]
        });
    }
});

type FormData = z.infer<typeof schema>;

export default function Register() {
    const { register, handleSubmit, formState: { errors, isValid } } = useForm<FormData>({ resolver: zodResolver(schema) });
    const [errorMessage, setErrorMessage] = useState('');
    const onSubmit = (data: FieldValues) => {
        const authService: AuthService = new AuthService(serverUrl);
        authService.register(data.email, data.password, data.confirmPassword).then((response) => {
            console.log(response);
            setErrorMessage('');
            if (response.data.isSuccess) {
                localStorage.setItem('jwt', response.data.result);
            }
            else {
                setErrorMessage(response.data.errorMessage);
            }
        });
    }

    return <>
        <AuthLayout header="Register">
            <form className="auth-form" onSubmit={handleSubmit(onSubmit)}>
                <div className="mb-3">
                    <input {...register('email')} type="email" placeholder="Email" className="form-control" />
                    {errors.email && <p className="text-danger">{ errors.email.message }</p>}
                </div>
                <div className="mb-3">
                    <input {...register('password')} type="password" placeholder="Password" className="form-control" />
                    {errors.password && <p className="text-danger">{ errors.password.message }</p>}
                </div>
                <div className="mb-3">
                    <input {...register('confirmPassword')} type="password" placeholder="Confirm Password" className="form-control" />
                    {errors.confirmPassword && <p className="text-danger">{errors.confirmPassword.message}</p>}
                </div>
                <p className="text-danger">{ errorMessage }</p>
                <div className="login-form-actions">
                    <button disabled={!isValid} className="btn btn-primary btn-lg login-btn">Register</button>
                </div>
            </form>
        </AuthLayout>
    </>
}