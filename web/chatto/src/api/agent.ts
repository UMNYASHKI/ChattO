import axios, { AxiosResponse } from "axios";
import { AddUsersToGroupRequest, CreateAccountRequest, CreateBillingInfoRequest, CreateFeedRequest, CreateGroupRequest, CreateOrganizationRequest, CreateTicketRequest, DeleteUsersFromGroupRequest, FeedFilterRequest, GetBillingInfosRequest, GetOrganizationBillingsRequest, GroupFilterRequest, LoginRequest, OrganizationFilteringRequest, TicketFilterRequest, UpdateFeedRequest, UpdateGroupRequest, UpdateOrganizationRequest, UpdateUserRequest, UserFilterRequest } from "./dtos/requests";
import { PagingResponse, GroupResponse, FeedResponse, FeedAppUserResponse, BillingInfoResponse, GetDetailsOrganizationResponse, UserDetailsResponse, UserResponse, TicketResponse, TicketDetailsResponse } from "./dtos/responses";

axios.defaults.baseURL = 'https://chatto.cloud/api'

const responseBody = (response: AxiosResponse) => response.data;

const requests = {
    get: (url: string, params?:{}) => axios.get(url, params).then(responseBody),
    post: (url: string, body: {}) => axios.post(url, body).then(responseBody),
    put: (url: string, body: {}) => axios.put(url, body).then(responseBody),
    delete: (url: string, body?: {}) => axios.delete(url, body).then(responseBody)
}

export const roles = ["SystemAdmin","Admin", "SuperAdmin", "User"];

export const jwt = () => localStorage.getItem("jwt");

export const setJwt = (token: string) => {
    axios.defaults.headers.common['Authorization'] = 'Bearer ' + token; 
    localStorage.setItem("jwt", token)
} 

export const account = {
    details: () => requests.get("/Account"),
    login: (data: LoginRequest) => requests.post("/Account/Login", data),
    logout: () => requests.get("/Account/Logout")
}

export const billing = {
    create: (id: string) => requests.post("/Billing?billingInfoId="+id, {}),
    get: (filter: GetOrganizationBillingsRequest) => requests.get("/Billing", {params:{filter}}),
    getById: (id: string) => requests.get("/Billing/" + id),
    delete: (id: string) => requests.delete("/Billing?billingId="+id)
}

export const billingInfo = {
    create: (data: CreateBillingInfoRequest):Promise<BillingInfoResponse> => requests.post("/BillingInfo", data),
    get: (filter: GetBillingInfosRequest):Promise<PagingResponse<BillingInfoResponse>> => requests.get("/BillingInfo", {params:{filter}}),
    getById: (id: string) => requests.get("/BillingInfo/" + id)
}

export const feed = {
    create: (data: CreateFeedRequest) => requests.post("/Feed", data),
    get: (filter: FeedFilterRequest):Promise<PagingResponse<FeedResponse>> => requests.get("/Feed", {params:{filter}}),
    getUsers: (id:string):Promise<FeedAppUserResponse[]> => requests.get(`/Feed/${id}/users`),
    getById: (id: string):Promise<FeedResponse> => requests.get("/Feed/" + id),
    /*update: (data: UpdateFeedRequest) => requests.post("/Feed", data),*/
    delete: (id: string) => requests.delete("/Feed/" + id),
}

export const group = {
    create: (data: CreateGroupRequest) => requests.post("/Group", data),
    get: (filter: GroupFilterRequest):Promise<PagingResponse<GroupResponse>> => requests.get("/Group", {params:{filter}}),
    getById: (id: string):Promise<GroupResponse> => requests.get("/Group/" + id),
    update: (id:string, data: UpdateGroupRequest) => requests.put("/Group/"+id, data),
    delete: (id: string) => requests.delete("/Group/" + id),
}

export const organization = {
    create: (data: CreateOrganizationRequest) => requests.post("/Organization", data),
    get: (filter: OrganizationFilteringRequest):Promise<PagingResponse<GetDetailsOrganizationResponse>> => requests.get("/Organization", {params:{filter}}),
    getById: (id: string):Promise<GetDetailsOrganizationResponse> => requests.get("/Organization/" + id),
    update: (id:string, data: UpdateOrganizationRequest) => requests.put("/Organization/"+id, data),
    delete: (id: string) => requests.delete("/Organization/" + id),
}

export const user = {
    create: (data: CreateAccountRequest) => requests.post("/User", data),
    get: (filter: UserFilterRequest):Promise<PagingResponse<UserResponse>> => requests.get("/User", {params:{filter}}),
    getById: (id: string):Promise<UserDetailsResponse> => requests.get("/User/" + id, {}),
    /*update: (id:string, data: UpdateUserRequest) => requests.post("/User/"+id, data),*/
    delete: (id: string) => requests.delete("/User/" + id),
}

export const userGroup = {
    create: (data: AddUsersToGroupRequest) => requests.post("/UserGroup", data),
    delete: (data: DeleteUsersFromGroupRequest) => requests.delete("/UserGroup", data),
}

export const ticket = {
    create: (data: CreateTicketRequest) => requests.post("/Ticket", data),
    get: (filter: TicketFilterRequest):Promise<PagingResponse<TicketResponse>> => requests.get("/Ticket", {params:{filter}}),
    getById: (id: string):Promise<TicketDetailsResponse> => requests.get("/Ticket/" + id),
    /*update: (id:string, data: UpdateOrganizationRequest) => requests.put("/Ticket/"+id, data),*/
    delete: (id: string) => requests.delete("/Ticket/" + id),
} //unimplemented