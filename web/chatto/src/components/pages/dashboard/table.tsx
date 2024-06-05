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
import { ReactNode, useEffect, useState } from 'react';

import { Button } from '@/components/ui/button';
import {
	Drawer,
	DrawerClose,
	DrawerContent,
	DrawerFooter,
	DrawerHeader,
	DrawerTitle,
	DrawerTrigger
} from '@/components/ui/drawer';
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

interface DataTableProps<TData, TValue> {
	state: 'idle' | 'loading' | 'error';
	columns: ColumnDef<TData, TValue>[];
	data: TData[];
	pagination: {
		total: number;
		pageSize: number;
		startingPage: number;
	};
	addForm: ReactNode;
	onSearch: (query: string) => void;
	onPagination: (page: number) => void;
}

export function DataTable<TData, TValue>({
	state,
	columns,
	data,
	pagination,
	addForm,
	onSearch,
	onPagination
}: DataTableProps<TData, TValue>) {
	const [sorting, setSorting] = useState<SortingState>([]);
	const [columnFilters, setColumnFilters] = useState<ColumnFiltersState>([]);
	const [query, setQuery] = useState<string>();

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
		<div className="flex flex-col justify-between h-full">
			<div className="flex items-center py-4 space-x-2">
				<Input
					placeholder="Search"
					onChange={(event) => setQuery(event.target.value)}
				/>
				<Button onClick={() => query !== undefined && onSearch(query)}>
					Search
				</Button>
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
			<div className="h-[70vh]">
				<div className="rounded-md border max-h-full overflow-auto">
					{state == 'loading' ? (
						<div className="flex justify-center items-center h-36">
							<div className="animate-spin rounded-full h-16 w-16 border-t-4"></div>
						</div>
					) : (
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
																header.column
																	.columnDef
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
												row.getIsSelected() &&
												'selected'
											}
										>
											{row
												.getVisibleCells()
												.map((cell) => (
													<TableCell key={cell.id}>
														{flexRender(
															cell.column
																.columnDef.cell,
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
					)}
				</div>
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
				<Drawer>
					<DrawerTrigger className="inline-flex items-center justify-center whitespace-nowrap rounded-md text-sm font-medium ring-offset-background transition-colors focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring focus-visible:ring-offset-2 disabled:pointer-events-none disabled:opacity-50 bg-primary text-primary-foreground hover:bg-primary/90 h-10 px-4 py-2">
						Add
					</DrawerTrigger>
					<DrawerContent className="lg:w-2/3 mx-auto">
						<DrawerHeader>
							<DrawerTitle className="text-4xl">
								Add entries to db
							</DrawerTitle>
							{/* <DrawerDescription>{description}</DrawerDescription> */}
						</DrawerHeader>
						<div className="px-4">{addForm}</div>
						<DrawerFooter className="flex-row">
							<DrawerClose className="inline-flex items-center justify-center whitespace-nowrap rounded-md text-sm font-medium ring-offset-background transition-colors focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring focus-visible:ring-offset-2 disabled:pointer-events-none disabled:opacity-50 border border-input bg-background hover:bg-accent hover:text-accent-foreground h-10 px-4 py-2 w-full">
								Close
							</DrawerClose>
						</DrawerFooter>
					</DrawerContent>
				</Drawer>
			</div>
		</div>
	);
}
