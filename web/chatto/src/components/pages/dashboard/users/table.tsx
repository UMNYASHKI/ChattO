'use client';

import { useEffect, useState } from 'react';

import { user } from '@/lib/api/agent';
import { PagingResponse, UserResponse } from '@/lib/api/resp';

import { DashboardError } from '../error';
import { DataTable } from '../table';
import { columns, ToTableUser } from './columns';

export const DashboardUserTable: React.FC<{
	orgId: string;
}> = ({ orgId }) => {
	const [query, setQuery] = useState('');
	const [page, setPage] = useState(0);

	const [users, setUsers] = useState<PagingResponse<UserResponse>>();
	const [state, setState] = useState<'error' | 'loading' | 'idle'>('loading');

	useEffect(() => {
		async function fetchData() {
			const u = await user.get({
				groupId: null,
				userName: null,
				appUserRole: 3,
				email: null,
				organizationId: orgId,
				isEmailSent: null,
				displayName: query,
				columnName: null,
				descending: null,
				pageNumber: page + 1,
				pageSize: 2
			});

			setUsers(u.json);

			if (!u.json) setState('error');
			else setState('idle');
		}
		fetchData();
	}, [orgId, query, page]);

	if (state == 'error')
		return <DashboardError message="Could not retrieve users" />;

	if (state == 'loading' || !users)
		return (
			<div className="flex justify-center items-center h-screen">
				<div className="animate-spin rounded-full h-16 w-16 border-t-4"></div>
			</div>
		);

	return (
		<div className="w-full h-full">
			<DataTable
				columns={columns}
				data={users.items.map(ToTableUser)}
				pagination={{
					total: users.totalCount,
					pageCount: users.totalPages,
					//api returns 1
					startingPage: users.currentPage - 1,
					pageSize: users.pageSize
				}}
				onSearch={setQuery}
				onPagination={setPage}
			/>
		</div>
	);
};
