export const DashboardError: React.FC<{ message: string }> = ({ message }) => {
	return (
		<div>
			<span>There was an unexpected error</span>
			<span>{message}</span>
		</div>
	);
};
