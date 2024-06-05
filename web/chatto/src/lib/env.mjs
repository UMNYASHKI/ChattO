import zod from 'zod';

const envSchema = zod.object({
	SESSION_SALT: zod.string({ message: 'No session salt in env' }),
	SESSION_PASS: zod.string({ message: 'No session salt in env' }),
	NEXT_PUBLIC_API_ENDPOINT: zod.string({ message: 'No api endpoint in env' })
});

export const env = envSchema.parse({
	SESSION_SALT: process.env.SESSION_SALT,
	SESSION_PASS: process.env.SESSION_PASS,
	NEXT_PUBLIC_API_ENDPOINT: process.env.NEXT_PUBLIC_API_ENDPOINT
});

if (typeof window !== 'undefined') {
	throw new Error(
		'src/lib/env.mjs should not be imported on the CLIENT. YOU ARE EXPOSING SECURITY KEYS.'
	);
}
