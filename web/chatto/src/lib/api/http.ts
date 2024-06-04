import { getSession } from '../actions/session';

type Headers = { [key: string]: string };

interface FetchOptions {
	method?: string;
	headers?: Headers;
	body?: unknown;
}

interface ApiInstanceOptions {
	baseURL: string;
	headers?: Headers;
}

export interface ApiResponse<T> {
	status: number;
	statusText?: string;
	ok: boolean;
	json?: T;
}

export function createApiInstance(options: ApiInstanceOptions) {
	return async <T = unknown>(
		endpoint: string,
		fetchOptions: FetchOptions = {}
	): Promise<ApiResponse<T>> => {
		const { baseURL, headers: defaultHeaders } = options;
		const { method = 'GET', headers = {}, body } = fetchOptions;

		const session = await getSession();

		const addToUrl =
			method == 'GET'
				? '?' + new URLSearchParams(JSON.stringify(body)).toString()
				: '';

		const _body = method != 'GET' ? { body: JSON.stringify(body) } : {};

		const response = await fetch(`${baseURL}${endpoint}` + addToUrl, {
			method,
			headers: {
				...defaultHeaders,
				...headers,
				Authorization: 'Bearer ' + session.token
			},
			..._body
		});

		let js;
		try {
			js = await response.json();
		} catch {}

		return {
			status: response.status,
			ok: response.ok,
			statusText: response.statusText,
			json: js
		};
	};
}

export const apiInstance = createApiInstance({
	baseURL: 'https://chatto.cloud/api',
	headers: {
		'Content-Type': 'application/json'
	}
});
