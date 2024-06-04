import { DashboardError } from '@/components/pages/dashboard/error';
import { Button } from '@/components/ui/button';
import { getSession } from '@/lib/actions/session';
import { billing } from '@/lib/api/agent';

const SubsPage: React.FC = async () => {
	const session = await getSession();

	if (!session.user)
		return <DashboardError message="Could not retrieve session" />;

	const resBilling = await billing.get({
		organizationId: session.user.organization.id,
		columnName: null,
		descending: null,
		pageNumber: 1,
		pageSize: 5
	});

	console.log(resBilling);
	return (
		<>
			<h1 className="text-4xl text-medium">Subscriptions</h1>
			<div className="w-full mt-12">
				<div className="w-full flex space-x-8 *:bg-neutral-200 *:rounded-xl text-2xl *:p-12">
					<div className="w-full space-x-4 flex justify-between">
						<div className="space-y-4">
							<span>Plan</span>
							<div>
								<span className="text-4xl">$100</span>
								<span className="font-extralight">
									/3 month
								</span>
							</div>
						</div>
						<button className="h-full bg-black text-white px-4 rounded-xl">
							Renew plan
						</button>
					</div>
					<div className="w-full space-y-4">
						<p>Expiration date</p>
						<p className="text-4xl">On June 30, 2024</p>
					</div>
				</div>
			</div>
		</>
	);
};

export default SubsPage;
