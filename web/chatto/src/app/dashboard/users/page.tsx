import { DashboardError } from '@/components/pages/dashboard/error';
import { DashboardUserTable } from '@/components/pages/dashboard/users/table';
import { getSession } from '@/lib/actions/session';

const UsersPage: React.FC = async () => {
	const session = await getSession();

	if (!session.user)
		return <DashboardError message="Could not retrieve session" />;

	return (
		<>
			<h1 className="text-4xl text-medium">Users</h1>
			<DashboardUserTable orgId={session.user.organization.id} />
		</>
	);
};

export default UsersPage;
