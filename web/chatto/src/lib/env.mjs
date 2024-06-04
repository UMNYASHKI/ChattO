import zod from 'zod';

const envSchema = zod.object({
	SESSION_SALT: zod.string({ message: 'No session salt in env' }),
	SESSION_PASS: zod.string({ message: 'No session salt in env' })
});

export const env = envSchema.parse({
	SESSION_SALT: process.env.SESSION_SALT,
	SESSION_PASS: process.env.SESSION_PASS
});

if (typeof window !== 'undefined') {
	// Come up with your own helpful error :)
	throw new Error(
		'src/server/config.js should not be imported on the frontend!'
	);
}
