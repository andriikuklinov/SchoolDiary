import React, { SyntheticEvent, useState } from "react";
import AuthLayout from "../auth_layout/AuthLayout";
import { z } from 'zod';
import { FieldValue, FieldValues, useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { AuthService } from "../../api/AuthService";
import { serverUrl } from "../../api/Api.module";
import { useNavigate, useParams, useSearchParams } from "react-router-dom";

const schema = z.object({
    password: z.string().nonempty(),
    confirmPassword: z.string().nonempty()
}).superRefine(({ password, confirmPassword }, ctx) => {
    if (password !== confirmPassword) {
        ctx.addIssue({
            code: "custom",
            message: "The confirmation of password doesn`t match",
            path: ["confirmPassword"]
        })
    }
});

type FormData = z.infer<typeof schema>;
export default function ForgotPassword() {
    const { register, handleSubmit, formState: { errors, isValid } } = useForm<FormData>({ resolver: zodResolver(schema) });
    const [errorMessage, setErrorMessage] = useState('');
    const [searchParams, setSearchParams] = useSearchParams();
    const navigate = useNavigate();
    const onSubmit = (data: FieldValues) => {
        const authService: AuthService = new AuthService(serverUrl);
        if (searchParams.get('resultToken') != null && searchParams.get('email') != null) {
            authService.resetPassword(encodeURIComponent(searchParams.get('resultToken')|| ''), searchParams.get('email'), data.password, data.confirmPassword).then((response) => {
                navigate('/login');
            }).catch((reason) => {
                console.log(reason);
            });
        } else {
            setErrorMessage('Something went wrong.');
        }
    }

    return <>
        <AuthLayout header="Forgot Password">
            <form className="auth-form" onSubmit={handleSubmit(onSubmit)}>
                <div className="mb-3">
                    <input {...register('password')} type="password" placeholder="Password" className="form-control" />
                    {errors.password && <p className="text-danger">{errors.password.message}</p>}
                </div>
                <div className="mb-3">
                    <input {...register('confirmPassword')} type="password" placeholder="Confirm Password" className="form-control" />
                    {errors.confirmPassword && <p className="text-danger">{errors.confirmPassword.message}</p>}
                </div>
                <p className="text-danger">{errorMessage}</p>
                <div className="auth-form-actions">
                    <button disabled={!isValid} className="btn btn-primary btn-lg login-btn">Set Password</button>
                </div>
            </form>
        </AuthLayout>
    </>
}