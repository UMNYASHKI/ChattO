'use client';

import { zodResolver } from '@hookform/resolvers/zod';
import { useFormState } from 'react-dom';
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
import { signup } from '@/lib/actions/signup';

const formSchema = z.object({
	organizationName: z.string().min(1, {
		message: 'Organization Name is required.'
	}),
	name: z.string().min(1, {
		message: 'Name is required.'
	}),
	email: z.string().email({
		message: 'Please enter a valid email address.'
	}),
	password: z.string().min(6, {
		message: 'Password must be at least 6 characters.'
	})
});

export const SignUpForm: React.FC = () => {
	const [state, dispatch] = useFormState(signup, undefined);

	const form = useForm<z.infer<typeof formSchema>>({
		resolver: zodResolver(formSchema),
		defaultValues: {
			organizationName: '',
			name: '',
			email: '',
			password: ''
		}
	});

	return (
		<Form {...form}>
			<form
				className="space-y-4 bg-neutral-100 p-12 w-[50rem] rounded-xl flex flex-col"
				action={dispatch}
			>
				<span className="mx-auto text-2xl">Registration</span>
				<FormField
					control={form.control}
					name="organizationName"
					render={({ field }) => (
						<FormItem>
							<FormLabel>Organization Name</FormLabel>
							<FormControl>
								<Input
									placeholder="Organization Name"
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
					name="name"
					render={({ field }) => (
						<FormItem>
							<FormLabel>Name</FormLabel>
							<FormControl>
								<Input
									placeholder="Name"
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
					name="email"
					render={({ field }) => (
						<FormItem>
							<FormLabel>Email</FormLabel>
							<FormControl>
								<Input
									placeholder="Email"
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
									placeholder="Password"
									type="password"
									{...field}
									className="rounded-full"
								/>
							</FormControl>
							<FormMessage />
						</FormItem>
					)}
				/>
				<Button type="submit" className="w-1/2 rounded-full mx-auto ">
					Register
				</Button>
			</form>
		</Form>
	);
};
