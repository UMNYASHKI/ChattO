'use client';

import Link from 'next/link';
import { useRouter } from 'next/navigation';

import Logo from '@/components/common/logo';
import { setSession } from '@/lib/actions/session';
import { account } from '@/lib/api/agent';

export const DashboardNav: React.FC = () => {
	const router = useRouter();
	const onLogOut = async () => {
		await account.logout();
		await setSession({ token: '' });
		router.push('/signin');
	};
	return (
		<nav className="bg-black p-8 text-white flex flex-col">
			<Link href="/" className="flex items-center space-x-4">
				<Logo className="h-12 w-12 fill-white" />
				<span>ChattO</span>
			</Link>
			<ul className="space-y-4 flex-1 mt-16">
				<li>
					<Link href="/dashboard">Dashboard</Link>
				</li>
				<li>
					<Link href="/dashboard/users">Users</Link>
				</li>
				<li>
					<Link href="/dashboard/groups">Groups</Link>
				</li>
				<li>
					<Link href="/dashboard/tickets">Tickets</Link>
				</li>
				<li>
					<Link href="/dashboard/subscriptions">Subscriptions</Link>
				</li>
			</ul>
			<ul className="space-y-4">
				<li>
					<Link href="/dashboard/settings">Settings</Link>
				</li>
				<li>
					<button onClick={() => onLogOut()}>Log Out</button>
				</li>
			</ul>
		</nav>
	);
};
