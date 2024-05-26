export enum AppUserRole {
    SystemAdmin,
    SuperAdmin,
    Admin,
    User
}

export enum BillingStatus {
    Pending,
    Captured
}

export enum BillingType {
    Quarter,
    Annual
}

export enum Currency {
    USD
}

export enum FeedType {
    Chat,
    Channel
}

export enum TicketStatus {
    NotStarted,
    InProgress,
    Completed
}

export enum TicketTheme {
    GroupActions,
    UserActions,
    Other
}

interface PagingParams {
    pageNumber: number;
    pageSize: number;
}

interface LoginRequest {
    email: string;
    password: string;
}

interface CreateBillingRequest {
    organizationId: string;
    billingInfoId: string;
}

interface CreateBillingInfoRequest {
    type: BillingType;
    price: number;
    currency: Currency;
}

interface GetOrganizationBillingsRequest extends SortingParams {
    organizationId: string;
}

 interface GetBillingInfosRequest extends SortingParams {
    type: BillingType | null;
    price: number | null;
}

 interface AppUserFeedRequest {
    appUserId: string;
}

 interface CreateFeedRequest {
    name: string;
    description: string;
    type: FeedType;
    groupId: string | null;
    appUsersId: string[];
}

 interface FeedFilterRequest extends SortingParams {
    name: string | null;
    description: string | null;
    type: FeedType | null;
    groupId: string | null;
}

 interface UpdateFeedRequest {
    name: string | null;
    description: string | null;
}

 interface CreateGroupRequest {
    name: string;
    organizationId: string;
    usersId: string[];
}

 interface GroupFilterRequest extends SortingParams {
    name: string | null;
    organizationId: string | null;
}

 interface UpdateGroupRequest {
    name: string | null;
}

 interface CreateOrganizationRequest {
    name: string;
    domain: string;
    userName: string;
    email: string;
    password: string;
}

 interface OrganizationFilteringRequest extends SortingParams {
    name: string | null;
    domain: string | null;
}

 interface UpdateOrganizationRequest {
    name: string;
    domain: string;
}

 interface CreateTicketRequest {
    text: string;
    theme: TicketTheme | null;
}

 interface TicketFilterRequest extends SortingParams {
    text: string | null;
    sentAt: string | null;
    theme: TicketTheme | null;
    appUserId: string | null;
}

 interface UpdateTicketRequest {
    text: string | null;
    theme: TicketTheme | null;
    status: TicketStatus | null;
}

 interface CreateAccountRequest {
    firstName: string;
    lastName: string;
    userName: string;
    email: string;
}

 interface CreateAccountsRequest {
    requests: CreateAccountRequest[];
    appUserRole: AppUserRole;
    organizationId: string;
}

 interface GetUsersByNameRequest extends SortingParams {
    displayName: string;
}

 interface UpdateUserRequest {
    displayName: string | null;
    email: string | null;
    userName: string | null;
}

 interface UserFilterRequest extends SortingParams {
    groupId: string | null;
    userName: string | null;
    appUserRole: AppUserRole | null;
    email: string | null;
    organizationId: string | null;
    isEmailSent: boolean | null;
    displayName: string | null;
}

 interface UserInOrganizationFilteringRequest extends SortingParams {
    groupId: string | null;
    userName: string | null;
    email: string | null;
    isEmailSent: boolean | null;
    displayName: string | null;
}

 interface AddUsersToGroupRequest {
    groupId: string;
    usersId: string[];
}

 interface DeleteUsersFromGroupRequest {
    groupId: string;
    usersId: string[];
}

 interface SortingParams extends PagingParams {
    columnName: string | null;
    descending: boolean | null;
}

