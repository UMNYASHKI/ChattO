import { AppUserRole, BillingType, FeedType, TicketStatus, TicketTheme } from "./requests";

 interface BillingInfoResponse {
    id: string;
    type: BillingType;
    price: number;
}

 interface BillingResponse {
    id: string;
    createdAt: string;
    organizationId: string;
    billingInfo: BillingInfoResponse;
}

 interface FeedAppUserResponse {
    id: string;
    displayName: string;
    email: string;
    isCreator: boolean;
}

 interface FeedResponse {
    feedId: string;
    name: string;
    description: string;
    type: FeedType;
    groupId: string | null;
    feedImageUrl: string | null;
}

 interface FileResponse {
    id: string;
    name: string;
    url: string;
}

 interface AppUserGroupResponse {
    id: string;
    userInGroupResponse: UserInGroupResponse;
    isModerator: boolean;
    groupId: string;
}

 interface GroupResponse {
    id: string;
    name: string;
    users: AppUserGroupResponse[];
}

 interface UserInGroupResponse {
    email: string;
    displayName: string;
}

 interface GetDetailsOrganizationResponse {
    id: string;
    name: string;
    domain: string;
}

 interface TicketDetailsResponse {
    id: string;
    text: string;
    sentAt: string;
    theme: TicketTheme;
    status: TicketStatus;
    appUser: UserResponse;
}

 interface TicketResponse {
    id: string;
    text: string;
    sentAt: string;
    theme: TicketTheme;
    status: TicketStatus;
}

 interface UserDetailsResponse {
    id: string;
    userName: string;
    email: string;
    displayName: string;
    role: AppUserRole;
    isEmailSent: boolean;
    organization: GetDetailsOrganizationResponse;
    profileImage: FileResponse;
}

 interface UserResponse {
    id: string;
    userName: string;
    email: string;
    displayName: string;
    role: AppUserRole;
    isEmailSent: boolean;
    profileImage: FileResponse;
}

 interface PagingResponse<T> {
    items: T[];
    currentPage: number;
    totalPages: number;
    pageSize: number;
    totalCount: number;
}