'use client';

import {
	ColumnDef,
	ColumnFiltersState,
	flexRender,
	getCoreRowModel,
	getSortedRowModel,
	PaginationState,
	SortingState,
	useReactTable,
	VisibilityState
} from '@tanstack/react-table';
import Link from 'next/link';
import { useEffect, useState } from 'react';

import { Button } from '@/components/ui/button';
import {
	DropdownMenu,
	DropdownMenuCheckboxItem,
	DropdownMenuContent,
	DropdownMenuTrigger
} from '@/components/ui/dropdown-menu';
import { Input } from '@/components/ui/input';
import {
	Table,
	TableBody,
	TableCell,
	TableHead,
	TableHeader,
	TableRow
} from '@/components/ui/table';
import useDebounce from '@/lib/hooks/debounce';

interface DataTableProps<TData, TValue> {
	columns: ColumnDef<TData, TValue>[];
	data: TData[];
	pagination: {
		total: number;
		pageCount: number;
		pageSize: number;
		startingPage: number;
	};
	onSearch: (query: string) => void;
	onPagination: (page: number) => void;
}

export function DataTable<TData, TValue>({
	columns,
	data,
	pagination,
	onSearch,
	onPagination
}: DataTableProps<TData, TValue>) {
	const [sorting, setSorting] = useState<SortingState>([]);
	const [columnFilters, setColumnFilters] = useState<ColumnFiltersState>([]);

	const [query, setQuery] = useState<string>();
	const debouncedQuery = useDebounce(query, 500);

	useEffect(() => {
		debouncedQuery && onSearch(debouncedQuery);
	}, [debouncedQuery, onSearch]);

	const [columnVisibility, setColumnVisibility] = useState<VisibilityState>(
		{}
	);

	const [_pagination, setPagination] = useState<PaginationState>({
		pageIndex: pagination.startingPage,
		pageSize: pagination.pageSize
	});

	useEffect(
		() => onPagination(_pagination.pageIndex),
		[_pagination, onPagination]
	);

	const table = useReactTable({
		data,
		columns,
		getCoreRowModel: getCoreRowModel(),
		// getPaginationRowModel: getPaginationRowModel(),
		manualPagination: true, //turn off client-side pagination
		rowCount: pagination.total,
		// pageCount: pagination.pageCount,
		onPaginationChange: setPagination, //update the pagination state when internal APIs mutate the pagination state
		onSortingChange: setSorting,
		getSortedRowModel: getSortedRowModel(),
		onColumnFiltersChange: setColumnFilters,
		// getFilteredRowModel: getFilteredRowModel(),
		manualFiltering: true,
		onColumnVisibilityChange: setColumnVisibility,
		state: {
			sorting,
			columnVisibility,
			columnFilters,
			pagination: _pagination
		}
	});

	return (
		<div>
			<div className="flex items-center py-4">
				<Input
					placeholder="Search"
					onChange={(event) => setQuery(event.target.value)}
				/>
				<DropdownMenu>
					<DropdownMenuTrigger asChild>
						<Button variant="outline" className="ml-auto">
							Columns
						</Button>
					</DropdownMenuTrigger>
					<DropdownMenuContent align="end">
						{table
							.getAllColumns()
							.filter((column) => column.getCanHide())
							.map((column) => {
								return (
									<DropdownMenuCheckboxItem
										key={column.id}
										className="capitalize"
										checked={column.getIsVisible()}
										onCheckedChange={(value) =>
											column.toggleVisibility(!!value)
										}
									>
										{column.id}
									</DropdownMenuCheckboxItem>
								);
							})}
					</DropdownMenuContent>
				</DropdownMenu>
			</div>
			<div className="rounded-md border">
				<Table>
					<TableHeader>
						{table.getHeaderGroups().map((headerGroup) => (
							<TableRow key={headerGroup.id}>
								{headerGroup.headers.map((header) => {
									return (
										<TableHead key={header.id}>
											{header.isPlaceholder
												? null
												: flexRender(
														header.column.columnDef
															.header,
														header.getContext()
													)}
										</TableHead>
									);
								})}
							</TableRow>
						))}
					</TableHeader>
					<TableBody>
						{table.getRowModel().rows?.length ? (
							table.getRowModel().rows.map((row) => (
								<TableRow
									key={row.id}
									data-state={
										row.getIsSelected() && 'selected'
									}
								>
									{row.getVisibleCells().map((cell) => (
										<TableCell key={cell.id}>
											{flexRender(
												cell.column.columnDef.cell,
												cell.getContext()
											)}
										</TableCell>
									))}
								</TableRow>
							))
						) : (
							<TableRow>
								<TableCell
									colSpan={columns.length}
									className="h-24 text-center"
								>
									No results.
								</TableCell>
							</TableRow>
						)}
					</TableBody>
				</Table>
			</div>
			<div className="flex items-center justify-between py-4">
				<div className="flex items-center justify-end space-x-2">
					<Button
						variant="outline"
						size="sm"
						onClick={() =>
							setPagination({
								..._pagination,
								pageIndex: _pagination.pageIndex - 1
							})
						}
						disabled={!table.getCanPreviousPage()}
					>
						Previous
					</Button>
					{Array.from({ length: table.getPageCount() }).map(
						(_, i) => {
							return (
								<Button
									key={'page-' + i}
									variant={
										_pagination.pageIndex == i
											? 'default'
											: 'outline'
									}
									size="sm"
									onClick={() =>
										setPagination({
											..._pagination,
											pageIndex: i
										})
									}
								>
									{i + 1}
								</Button>
							);
						}
					)}
					<Button
						variant="outline"
						size="sm"
						onClick={() =>
							setPagination({
								..._pagination,
								pageIndex: _pagination.pageIndex + 1
							})
						}
						disabled={!table.getCanNextPage()}
					>
						Next
					</Button>
				</div>
				<Link
					href="/dashboard/users/add"
					className="p-1 px-3 border rounded-lg hover:bg-black hover:text-white transition"
				>
					Add
				</Link>
			</div>
		</div>
	);
}
