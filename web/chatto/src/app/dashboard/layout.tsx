import { DashboardNav } from '@/components/pages/dashboard/nav';

export default function DashboardLayout({
	children
}: {
	readonly children: React.ReactNode;
}) {
	return (
		<div className="h-screen flex">
			<DashboardNav />
			<main className="flex flex-col flex-1 w-full p-12">{children}</main>
		</div>
	);
}
