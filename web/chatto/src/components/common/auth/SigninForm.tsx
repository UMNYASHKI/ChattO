import React, { useState } from 'react';
import { useForm } from 'react-hook-form';

import { signin } from '@/api/auth';

import { AuthForm } from './AuthForm';

export const SigninForm: React.FC = () => {
	const [error, setError] = useState<string | null>(null);
	const {
		register,
		handleSubmit,
		formState: { errors }
	} = useForm();

	const onSubmit = async (data: { email: string; password: string }) => {
		try {
			await signin(data.email, data.password);
		} catch (error) {
			setError(error.message);
		}
	};

	return (
		<AuthForm
			onSubmit={handleSubmit(onSubmit)}
			register={register}
			errors={errors}
			error={error}
			title="Sign In"
			submitText="Sign In"
		/>
	);
};
