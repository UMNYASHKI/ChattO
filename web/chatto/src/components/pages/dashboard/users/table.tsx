'use client';

import { useEffect, useState } from 'react';

import { user } from '@/lib/api/agent';
import { PagingResponse, UserResponse } from '@/lib/api/resp';

import { DashboardError } from '../error';
import { DataTable } from '../table';
import { DashboardUsersAdd } from './add';
import { columns, ToTableUser } from './columns';

export const DashboardUserTable: React.FC<{
	orgId: string;
}> = ({ orgId }) => {
	const [query, setQuery] = useState('');
	const [page, setPage] = useState(0);

	const [users, setUsers] = useState<PagingResponse<UserResponse>>({
		items: [],
		currentPage: 1,
		totalPages: 0,
		pageSize: 10,
		totalCount: 1
	});
	const [state, setState] = useState<'error' | 'loading' | 'idle'>('loading');

	useEffect(() => {
		async function fetchData() {
			setState('loading');
			const req = await user.get({
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
				pageSize: 10
			});

			if (!req.json) setState('error');
			else {
				setUsers(req.json);
				setState('idle');
			}
		}
		fetchData();
	}, [orgId, query, page]);

	if (state == 'error')
		return <DashboardError message="Could not retrieve users" />;

	const data = users.items.map(ToTableUser);

	return (
		<div className="w-full flex-1">
			<DataTable
				state={state}
				columns={columns}
				data={data}
				pagination={{
					total: users.totalCount,
					//api returns 1 because we suck
					startingPage: users.currentPage - 1,
					pageSize: users.pageSize
				}}
				addForm={<DashboardUsersAdd />}
				onSearch={setQuery}
				onPagination={setPage}
			/>
		</div>
	);
};
