import Image from 'next/image';
import Link from 'next/link';

import { GetDetailsOrganizationResponse } from '@/lib/api/resp';

export const DashboardGrid: React.FC<{
	org?: GetDetailsOrganizationResponse;
	tickets?: number;
}> = ({ org, tickets }) => {
	return (
		<div
			className="w-full h-full grid gap-8 grid-cols-5 grid-rows-6 grid-rows
        *:bg-neutral-200 *:rounded-xl *:flex *:justify-center *:items-center"
		>
			<div className="col-span-2 row-span-4">Chatto</div>
			<Link href="/dashboard/users" className="col-span-1 row-span-2">
				Add user
			</Link>
			<div className="col-span-2 row-span-3">
				{org ? (
					<>
						{/* <Image
							src={org?.domain}
							width={200}
							height={200}
							alt="Organization logo"
						/> */}
						<span>{org?.name}</span>
					</>
				) : (
					<div></div>
				)}
			</div>
			<Link href="/dashboard/groups" className="col-span-1 row-span-2">
				Add group
			</Link>
			<div className="col-span-2 row-span-3">
				<span>???</span>
				<span>users</span>
			</div>
			<div className="col-span-3 row-span-2">
				<span>You have {tickets} unread tickets</span>
				<Link href="/tickets">Go to tickets</Link>
			</div>
		</div>
	);
};
