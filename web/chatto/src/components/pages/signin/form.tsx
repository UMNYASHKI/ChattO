'use client';

import { zodResolver } from '@hookform/resolvers/zod';
import Link from 'next/link';
import { useFormState, useFormStatus } from 'react-dom';
import { useForm } from 'react-hook-form';
import { z } from 'zod';

import { Button } from '@/components/ui/button';
import {
	Form,
	FormControl,
	FormField,
	FormItem,
	FormLabel,
	FormMessage
} from '@/components/ui/form';
import { Input } from '@/components/ui/input';
import { signin } from '@/lib/actions/singin';

const formSchema = z.object({
	email: z.string().email().min(2, {
		message: 'Username must be at least 2 characters.'
	}),
	password: z.string().min(2, {
		message: 'Password must be at least 2 characters.'
	})
});

export const SignInForm: React.FC = () => {
	const [state, dispatch] = useFormState(signin, undefined);

	const form = useForm<z.infer<typeof formSchema>>({
		resolver: zodResolver(formSchema),
		defaultValues: {
			email: '',
			password: ''
		}
	});

	return (
		<Form {...form}>
			<form
				action={dispatch}
				className="space-y-4 bg-neutral-100 p-12 w-[50rem] rounded-xl flex flex-col"
			>
				<span className="mx-auto text-2xl">Log In</span>
				<FormField
					control={form.control}
					name="email"
					render={({ field }) => (
						<FormItem>
							<FormLabel>Email</FormLabel>
							<FormControl>
								<Input
									placeholder="email"
									{...field}
									className="rounded-full"
								/>
							</FormControl>
							<FormMessage />
						</FormItem>
					)}
				/>
				<FormField
					control={form.control}
					name="password"
					render={({ field }) => (
						<FormItem>
							<FormLabel>Password</FormLabel>
							<FormControl>
								<Input
									placeholder="password"
									{...field}
									className="rounded-full"
								/>
							</FormControl>
							<FormMessage />
						</FormItem>
					)}
				/>
				<Link
					href="/forgot"
					className="block ml-auto underline underline-offset-4"
				>
					Forgot password?
				</Link>
				<LoginButton />
				<div className="mx-auto">
					<span>Don&apos;t have an account? </span>
					<Link
						href="/signup"
						className="underline underline-offset-4"
					>
						Sign up now
					</Link>
				</div>
			</form>
		</Form>
	);
};

function LoginButton() {
	const { pending } = useFormStatus();

	const handleClick = (event: { preventDefault: () => void }) => {
		if (pending) {
			event.preventDefault();
		}
	};

	return (
		<Button
			aria-disabled={pending}
			type="submit"
			onClick={handleClick}
			className="w-1/2 rounded-full mx-auto"
		>
			Login
		</Button>
	);
}
