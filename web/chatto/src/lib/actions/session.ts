'use server';

import { cookies } from 'next/headers';

import { UserDetailsResponse } from '../api/resp';
import { decrypt, encrypt } from '../crypto';
import { env } from '../env.mjs';

const pass = env.SESSION_PASS;

export interface Session {
	user?: UserDetailsResponse;
	token: string;
}

export async function setSession(sessionData: Session) {
	const encryptedSessionData = await encrypt(
		JSON.stringify(sessionData),
		pass
	);
	cookies().set('session', encryptedSessionData, {
		httpOnly: true,
		secure: process.env.NODE_ENV === 'production',
		maxAge: 60 * 60, // One hour
		path: '/'
	});
	return encryptedSessionData;
}

export async function getSession(): Promise<Session> {
	const encryptedSessionData = cookies().get('session')?.value;
	if (!encryptedSessionData) return { token: '' };
	try {
		const decypted = await decrypt(encryptedSessionData, pass);
		return JSON.parse(decypted);
	} catch (e) {
		console.log(e);
	}
	return { token: '' };
}
