import { DashboardGrid } from '@/components/pages/dashboard/index';
import { getSession } from '@/lib/actions/session';

const DashboardPage: React.FC = async () => {
	const session = await getSession();

	return (
		<>
			<DashboardGrid tickets={4} org={session.user?.organization} />
		</>
	);
};

export default DashboardPage;
