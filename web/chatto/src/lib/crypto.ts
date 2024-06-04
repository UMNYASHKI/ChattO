import { env } from './env.mjs';

const salt = () => env.SESSION_SALT;

// Utility function to convert ArrayBuffer to Hex String
function bufferToHex(buffer: ArrayBuffer): string {
	return Array.prototype.map
		.call(new Uint8Array(buffer), (x) => ('00' + x.toString(16)).slice(-2))
		.join('');
}

// Utility function to convert Hex String to ArrayBuffer
function hexToBuffer(hex: string): ArrayBuffer {
	const bytes = new Uint8Array(hex.length / 2);
	for (let i = 0; i < hex.length; i += 2) {
		bytes[i / 2] = parseInt(hex.substr(i, 2), 16);
	}
	return bytes.buffer;
}

// Generate a CryptoKey from a password
async function generateKey(password: string, salt: string): Promise<CryptoKey> {
	const enc = new TextEncoder();
	const keyMaterial = await crypto.subtle.importKey(
		'raw',
		enc.encode(password),
		{ name: 'PBKDF2' },
		false,
		['deriveKey']
	);

	return crypto.subtle.deriveKey(
		{
			name: 'PBKDF2',
			salt: enc.encode(salt),
			iterations: 100000,
			hash: 'SHA-256'
		},
		keyMaterial,
		{ name: 'AES-CBC', length: 256 },
		true,
		['encrypt', 'decrypt']
	);
}

export async function encrypt(text: string, password: string): Promise<string> {
	const enc = new TextEncoder();
	const iv = crypto.getRandomValues(new Uint8Array(16));
	const key = await generateKey(password, salt());
	const encrypted = await crypto.subtle.encrypt(
		{
			name: 'AES-CBC',
			iv: iv
		},
		key,
		enc.encode(text)
	);
	const ivHex = bufferToHex(iv.buffer);
	const encryptedHex = bufferToHex(encrypted);
	return ivHex + encryptedHex;
}

export async function decrypt(
	encryptedText: string,
	password: string
): Promise<string> {
	const ivHex = encryptedText.slice(0, 32);
	const dataHex = encryptedText.slice(32);
	const iv = hexToBuffer(ivHex);
	const data = hexToBuffer(dataHex);
	const key = await generateKey(password, salt());
	const decrypted = await crypto.subtle.decrypt(
		{
			name: 'AES-CBC',
			iv: new Uint8Array(iv)
		},
		key,
		new Uint8Array(data)
	);
	const dec = new TextDecoder();
	return dec.decode(decrypted);
}
