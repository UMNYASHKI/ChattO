'use server';

import { organization } from '../api/agent';
import { signin } from './singin';

export async function signup(_currentState: unknown, formData: FormData) {
	const organizationName = formData.get('organizationName')?.toString();
	const name = formData.get('name')?.toString();
	const email = formData.get('email')?.toString();
	const pass = formData.get('password')?.toString();

	if (!email || !pass || !organizationName || !name)
		return { s: false, m: 'Invalid credentials.' };

	try {
		const res = await organization.create({
			name: organizationName,
			domain: organizationName + '.dev',
			userName: name,
			email: email,
			password: pass
		});

		if (!res.ok) {
			return { s: false, m: 'Something went wrong. ' + res.statusText };
		}

		signin({}, formData);
	} catch (error) {
		throw error;
	}
}
