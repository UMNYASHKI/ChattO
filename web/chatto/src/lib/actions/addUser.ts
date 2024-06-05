'use server';

import { user } from '../api/agent';

export async function addUser(_currentState: unknown, formData: FormData) {
	const first = formData.get('first')?.toString();
	const last = formData.get('last')?.toString();
	const username = formData.get('username')?.toString();
	const email = formData.get('email')?.toString();

	if (!first || !username || !email || !last)
		return { s: false, m: 'Incomplete data' };

	try {
		const res = await user.create({
			firstName: first,
			lastName: last,
			userName: username,
			email: email
		});

		if (res.status == 400) {
			return { s: false, m: 'Session invalid or malformed input' };
		} else if (!res.ok || !res.json) {
			return { s: false, m: 'Something went wrong.' };
		}

		return { s: true, m: '' };
	} catch (error) {
		throw error;
	}
}
