import { DashboardSeattingsChangePassword } from '@/components/pages/dashboard/settings/changepass';
import { DashboardSeattingsConfirm } from '@/components/pages/dashboard/settings/delete';
import { getSession } from '@/lib/actions/session';

const SettingsPage: React.FC = async () => {
	const session = await getSession();

	return (
		<>
			<h1 className="text-4xl text-medium">Settings</h1>
			<div className="pt-4 space-y-4">
				<div>
					<p>Name of organization</p>
					<p className="p-4 bg-neutral-200 rounded-lg">
						{session.user?.organization.name}
					</p>
				</div>
				<div>
					<p>About organization</p>
					<p className="p-4 bg-neutral-200 rounded-lg">
						Step 1: decide to roll separate front/back. Fair enough.
						<br />
						Step 2: write the backend in c#... the mobile app in
						kotlin... and the website in typescript... alright...
						<br />
						Step 3: websockets voodoo cause why not
						<br />
						Step 4: custom auth system (why)
						<br />
						Step 5: server side pagination (why)
						<br />
						Step 6: Half of the api is dead
						<br />
						Step 7: sell as unicorn startup. or exit scam. idk.
					</p>
				</div>
				<DashboardSeattingsConfirm
					title="Delete"
					description="After deleting an organization, all data, messages and files will be deleted permanently."
				/>
				<DashboardSeattingsConfirm
					title="Transfer superadmin rights"
					description="After the transfer of superadmin rights, you will not have access to this account and the password will be emailed to the new superadmin."
				/>
				<DashboardSeattingsChangePassword />
			</div>
		</>
	);
};

export default SettingsPage;
