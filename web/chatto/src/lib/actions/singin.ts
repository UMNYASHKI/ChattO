'use server';

import { redirect } from 'next/navigation';

import { account, billing } from '../api/agent';
import { setSession } from './session';

export async function signin(_currentState: unknown, formData: FormData) {
	const email = formData.get('email')?.toString();
	const pass = formData.get('password')?.toString();

	if (!email || !pass) return { s: false, m: 'Invalid credentials.' };

	try {
		const res = await account.login({
			email: email,
			password: pass
		});

		if (res.status == 400) return { s: false, m: 'Invalid credentials.' };
		else if (!res.ok || !res.json?.token) {
			return { s: false, m: 'Something went wrong.' };
		}

		await setSession({
			token: res.json.token
		});

		const resUser = await account.details();

		if (resUser.status == 400 || !resUser.json?.organization.id) {
			return { s: false, m: 'Invalid token' };
		} else if (!resUser.ok) {
			return {
				s: false,
				m: 'Could not find user.'
			};
		}

		await setSession({
			token: res.json.token,
			user: resUser.json
		});

		// const resBilling = await billing.get({
		// 	organizationId: resUser.json?.organization.id,
		// 	columnName: null,
		// 	descending: null,
		// 	pageNumber: 0,
		// 	pageSize: 0
		// });

		// console.log(resBilling);

		// if (resBilling.status == 400 || !resBilling.ok) {
		// 	return {
		// 		s: false,
		// 		m: 'Something went wrong while retrieveing org sub'
		// 	};
		// }

		// // i hate all of you
		// const validSub = resBilling.json?.items.find((x) =>
		// 	isSubscriptionValid(x.createdAt, x.billingInfo.type)
		// );

		// console.log(validSub);

		// if (validSub != undefined) redirect('/dashboard');
		// else redirect('/pricing');

		redirect('/dashboard');
	} catch (error) {
		throw error;
	}
}

// importing this breaks next js
enum BillingType {
	Quarter,
	Annual
}

function isSubscriptionValid(startDate: string, type: BillingType): boolean {
	const start = new Date(startDate);
	const now = new Date();

	if (isNaN(start.getTime())) {
		throw new Error('Invalid date format');
	}

	let expirationDate: Date;

	switch (type) {
		case BillingType.Annual:
			expirationDate = new Date(start);
			expirationDate.setFullYear(start.getFullYear() + 1);
			break;
		case BillingType.Quarter:
			expirationDate = new Date(start);
			expirationDate.setMonth(start.getMonth() + 3);
			break;
		default:
			throw new Error('Invalid subscription type');
	}

	return now <= expirationDate;
}
