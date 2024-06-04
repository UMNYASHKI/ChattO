import { BillingInfoResponse, PagingResponse } from '@/lib/api/resp';

export const DashboardSubscriptionsHistory: React.FC<{
	subs: PagingResponse<BillingInfoResponse>;
	onPagination: (page: number) => void;
}> = () => {
	return (
		<div>
			<p>History</p>
			<div></div>
		</div>
	);
};
