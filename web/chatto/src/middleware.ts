import type { NextRequest } from 'next/server';

import { getSession } from './lib/actions/session';

export async function middleware(request: NextRequest) {
	const session = await getSession();

	if (session.user && request.nextUrl.pathname.startsWith('/signin')) {
		return Response.redirect(new URL('/dashboard', request.url));
	}

	if (!session.user && request.nextUrl.pathname.startsWith('/dashboard')) {
		return Response.redirect(new URL('/signin', request.url));
	}
}

export const config = {
	matcher: ['/((?!api|_next/static|_next/image|.*\\.png$).*)']
};
