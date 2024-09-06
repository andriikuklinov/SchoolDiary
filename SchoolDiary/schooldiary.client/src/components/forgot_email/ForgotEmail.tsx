import React, { useState } from "react";
import AuthLayout from "../auth_layout/AuthLayout";
import { z } from "zod";
import { FieldValues, useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { serverUrl } from "../../api/Api.module";
import { AuthService } from "../../api/AuthService";

const schema = z.object({
    email: z.string().email().nonempty()
});

type FormData = z.infer<typeof schema>;

export default function ForgotEmail() {
    const { register, handleSubmit, formState: { errors, isValid } } = useForm<FormData>({ resolver: zodResolver(schema) });
    const [errorMessage, setErrorMessage] = useState('');
    const onSubmit = (data: FieldValues) => {
        const authService: AuthService = new AuthService(serverUrl);
        authService.forgotPassword(data.email).then((response) => {
            setErrorMessage('');
            if (response.data.isSuccess) {

            }
            else {
                setErrorMessage(response.data.errorMessage);
            }
        });
    }
    return <>
        <AuthLayout header="Please enter email">
            <form className="auth-form" onSubmit={handleSubmit(onSubmit)}>
                <div className="mb-3">
                    <input {...register('email')} type="email" placeholder="Email" className="form-control" />
                    {errors.email && <p className="text-danger">{errors.email.message}</p>}
                </div>
                <div className="auth-form-actions">
                    <button disabled={!isValid} className="btn btn-primary btn-lg login-btn">Confirm</button>
                </div>
            </form>
        </AuthLayout>
    </>
}