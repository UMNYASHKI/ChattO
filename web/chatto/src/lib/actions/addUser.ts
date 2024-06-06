'use server';

import { user } from '../api/agent';
import { CreateAccountRequest, CreateAccountsRequest } from '../api/reqs';
import { getSession } from './session';

export async function addUser(_currentState: unknown, formData: FormData) {
	const first = formData.get('first')?.toString();
	const last = formData.get('last')?.toString();
	const username = formData.get('username')?.toString();
	const email = formData.get('email')?.toString();

	if (!first || !username || !email || !last)
		return { s: false, m: 'Incomplete data' };

	try {
		const session = await getSession();
		if (!session.user)
			return { s: false, m: 'Session invalid or malformed input' };

		const account: CreateAccountRequest = {
			firstName: first,
			lastName: last,
			userName: username,
			email: email
		};

		const req: CreateAccountsRequest = {
			requests: [account],
			appUserRole: 3,
			organizationId: session.user.organization.id
		};

		const res = await user.create(req);

		console.log(res);

		if (res.status == 400) {
			return { s: false, m: 'Session invalid or malformed input' };
		} else if (!res.ok) {
			return { s: false, m: 'Something went wrong.' };
		}

		return { s: true, m: '' };
	} catch (error) {
		throw error;
	}
}
