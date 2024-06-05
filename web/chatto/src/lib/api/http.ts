import { getSession } from '../actions/session';

type Headers = { [key: string]: string };

export interface FetchOptions {
	method?: string;
	headers?: Headers;
	body?: object;
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

		let fullUrl = `${baseURL}${endpoint}`;

		if (method === 'GET' && body) {
			const filteredBody = Object.fromEntries(
				Object.entries(body).filter(([_, value]) => value !== null)
			);
			const queryParams = new URLSearchParams(
				filteredBody as Record<string, string>
			);
			fullUrl += `?${queryParams.toString()}`;
		}

		const requestOptions: RequestInit = {
			method,
			headers: {
				...defaultHeaders,
				...headers,
				Authorization: 'Bearer ' + session.token
			}
		};

		if (method !== 'GET' && body) {
			requestOptions.body = JSON.stringify(body);
		}

		const response = await fetch(fullUrl, requestOptions);

		let js;
		try {
			js = await response.json();
		} catch {
			js = undefined;
		}

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
