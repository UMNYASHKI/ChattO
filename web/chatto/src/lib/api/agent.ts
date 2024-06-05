import { apiInstance, ApiResponse, FetchOptions } from './http';
import {
	AddUsersToGroupRequest,
	CreateAccountRequest,
	CreateBillingInfoRequest,
	CreateFeedRequest,
	CreateGroupRequest,
	CreateOrganizationRequest,
	CreateTicketRequest,
	DeleteUsersFromGroupRequest,
	FeedFilterRequest,
	GetBillingInfosRequest,
	GetOrganizationBillingsRequest,
	GroupFilterRequest,
	LoginRequest,
	OrganizationFilteringRequest,
	TicketFilterRequest,
	UpdateFeedRequest,
	UpdateGroupRequest,
	UpdateOrganizationRequest,
	UpdateUserRequest,
	UserFilterRequest
} from './reqs';
import {
	BillingInfoResponse,
	BillingResponse,
	FeedAppUserResponse,
	FeedResponse,
	GetDetailsOrganizationResponse,
	GroupResponse,
	LoginResponse,
	PagingResponse,
	TicketDetailsResponse,
	TicketResponse,
	UserDetailsResponse,
	UserResponse
} from './resp';

const requests = {
	get: <T>(url: string, params?: FetchOptions['body']) =>
		apiInstance<T>(url, { method: 'GET', body: params }),
	post: <T>(url: string, body?: FetchOptions['body']) =>
		apiInstance<T>(url, { method: 'POST', body: body }),
	put: <T>(url: string, body?: FetchOptions['body']) =>
		apiInstance<T>(url, { method: 'PUT', body: body }),
	delete: <T>(url: string, body?: FetchOptions['body']) =>
		apiInstance<T>(url, { method: 'DELETE', body: body })
};

export const roles = ['SystemAdmin', 'Admin', 'SuperAdmin', 'User'];

export const account = {
	details: (): Promise<ApiResponse<UserDetailsResponse>> =>
		requests.get('/Account'),
	login: (data: LoginRequest): Promise<ApiResponse<LoginResponse>> =>
		requests.post('/Account/Login', data),
	logout: () => {
		const resp = requests.get('/Account/Logout');

		return resp;
	},
	forgotPassword: (data: LoginRequest['email']) =>
		new Error('Not implemented')
};

export const billing = {
	get: (
		filter: GetOrganizationBillingsRequest
	): Promise<ApiResponse<PagingResponse<BillingResponse>>> =>
		requests.get('/Billing', { params: { filter } }),
	getById: (id: string) => requests.get('/Billing/' + id)
};

export const billingInfo = {
	create: (
		data: CreateBillingInfoRequest
	): Promise<ApiResponse<BillingInfoResponse>> =>
		requests.post('/BillingInfo', data),
	get: (
		filter: GetBillingInfosRequest
	): Promise<ApiResponse<PagingResponse<BillingInfoResponse>>> =>
		requests.get('/BillingInfo', { params: { filter } }),
	getById: (id: string) => requests.get('/BillingInfo/' + id)
};

export const feed = {
	create: (data: CreateFeedRequest) => requests.post('/Feed', data),
	get: (
		filter: FeedFilterRequest
	): Promise<ApiResponse<PagingResponse<FeedResponse>>> =>
		requests.get('/Feed', { params: { filter } }),
	getUsers: (id: string): Promise<ApiResponse<FeedAppUserResponse[]>> =>
		requests.get(`/Feed/${id}/users`),
	getById: (id: string): Promise<ApiResponse<FeedResponse>> =>
		requests.get('/Feed/' + id),
	/*update: (data: UpdateFeedRequest) => requests.post("/Feed", data),*/
	delete: (id: string) => requests.delete('/Feed/' + id)
};

export const group = {
	create: (data: CreateGroupRequest) => requests.post('/Group', data),
	get: (
		filter: GroupFilterRequest
	): Promise<ApiResponse<PagingResponse<GroupResponse>>> =>
		requests.get('/Group', { params: { filter } }),
	getById: (id: string): Promise<ApiResponse<GroupResponse>> =>
		requests.get('/Group/' + id),
	update: (id: string, data: UpdateGroupRequest) =>
		requests.put('/Group/' + id, data),
	delete: (id: string) => requests.delete('/Group/' + id)
};

export const organization = {
	create: (data: CreateOrganizationRequest) =>
		requests.post('/Organization', data),
	get: (
		filter: OrganizationFilteringRequest
	): Promise<ApiResponse<PagingResponse<GetDetailsOrganizationResponse>>> =>
		requests.get('/Organization', { params: { filter } }),
	getById: (
		id: string
	): Promise<ApiResponse<GetDetailsOrganizationResponse>> =>
		requests.get('/Organization/' + id),
	update: (id: string, data: UpdateOrganizationRequest) =>
		requests.put('/Organization/' + id, data),
	delete: (id: string) => requests.delete('/Organization/' + id)
};

export const user = {
	create: (data: CreateAccountRequest) => requests.post('/User', data),
	get: (
		filter: UserFilterRequest
	): Promise<ApiResponse<PagingResponse<UserResponse>>> =>
		requests.get('/User', filter),
	getById: (id: string): Promise<ApiResponse<UserDetailsResponse>> =>
		requests.get('/User/' + id, {}),
	/*update: (id:string, data: UpdateUserRequest) => requests.post("/User/"+id, data),*/
	delete: (id: string) => requests.delete('/User/' + id)
};

export const userGroup = {
	create: (data: AddUsersToGroupRequest) => requests.post('/UserGroup', data),
	delete: (data: DeleteUsersFromGroupRequest) =>
		requests.delete('/UserGroup', data)
};

export const ticket = {
	create: (data: CreateTicketRequest) => requests.post('/Ticket', data),
	get: (
		filter: TicketFilterRequest
	): Promise<ApiResponse<PagingResponse<TicketResponse>>> =>
		requests.get('/Ticket', { params: { filter } }),
	getById: (id: string): Promise<ApiResponse<TicketDetailsResponse>> =>
		requests.get('/Ticket/' + id),
	/*update: (id:string, data: UpdateOrganizationRequest) => requests.put("/Ticket/"+id, data),*/
	delete: (id: string) => requests.delete('/Ticket/' + id)
}; //unimplemented
