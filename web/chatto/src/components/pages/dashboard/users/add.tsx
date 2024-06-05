'use client';

import { zodResolver } from '@hookform/resolvers/zod';
import { useEffect } from 'react';
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
import { useToast } from '@/components/ui/use-toast';
import { addUser } from '@/lib/actions/addUser';

const formSchema = z.object({
	first: z.string().min(2, {
		message: 'first name must be at least 2 characters.'
	}),
	last: z.string().min(2, {
		message: 'last name must be at least 2 characters.'
	}),
	email: z.string().email().min(2, {
		message: 'email must be at least 2 characters.'
	}),
	username: z.string().min(2, {
		message: 'username must be at least 2 characters.'
	})
});

export const DashboardUsersAdd: React.FC = () => {
	const [result, dispatch] = useFormState(addUser, undefined);
	const data = useFormStatus();
	const { toast } = useToast();

	const form = useForm<z.infer<typeof formSchema>>({
		resolver: zodResolver(formSchema),
		defaultValues: {
			first: '',
			last: '',
			email: '',
			username: ''
		}
	});

	useEffect(() => {
		if (data.pending) return;
		if (!result) return;

		if (result.s) {
			toast({
				title: 'Added new user',
				description: 'One new user has been added.'
			});
		} else {
			toast({
				title: 'Failed adding new user',
				description: result?.m
			});
		}
	}, [result, data, toast]);

	return (
		<Form {...form}>
			<form action={dispatch}>
				<div className="flex *:flex-1 py-2 -space-x-4">
					<FormLabel>First name</FormLabel>
					<FormLabel>Last name</FormLabel>
					<FormLabel>Email</FormLabel>
					<FormLabel>Username</FormLabel>
					<div />
				</div>
				<div className="flex *:flex-1 items-end space-x-4">
					<FormField
						disabled={data.pending}
						control={form.control}
						name="first"
						render={({ field }) => (
							<FormItem>
								<FormControl>
									<Input
										placeholder="First name"
										{...field}
										className="rounded-md"
									/>
								</FormControl>
								<FormMessage />
							</FormItem>
						)}
					/>
					<FormField
						disabled={data.pending}
						control={form.control}
						name="last"
						render={({ field }) => (
							<FormItem>
								<FormControl>
									<Input
										placeholder="Last name"
										{...field}
										className="rounded-md"
									/>
								</FormControl>
								<FormMessage />
							</FormItem>
						)}
					/>
					<FormField
						disabled={data.pending}
						control={form.control}
						name="email"
						render={({ field }) => (
							<FormItem>
								<FormControl>
									<Input
										placeholder="Email"
										{...field}
										className="rounded-md"
									/>
								</FormControl>
								<FormMessage />
							</FormItem>
						)}
					/>
					<FormField
						disabled={data.pending}
						control={form.control}
						name="username"
						render={({ field }) => (
							<FormItem>
								<FormControl>
									<Input
										placeholder="Username"
										{...field}
										className="rounded-md"
									/>
								</FormControl>
								<FormMessage />
							</FormItem>
						)}
					/>
					<SubmitButton />
				</div>
			</form>
		</Form>
	);
};

function SubmitButton() {
	const { pending } = useFormStatus();

	const handleClick = (event: { preventDefault: () => void }) => {
		if (pending) event.preventDefault();
	};

	return (
		<Button
			aria-disabled={pending}
			type="submit"
			onClick={handleClick}
			className="flex-none rounded-full"
		>
			Add
		</Button>
	);
}
