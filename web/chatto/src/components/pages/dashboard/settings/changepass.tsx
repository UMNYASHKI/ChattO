import { Button } from '@/components/ui/button';
import {
	Drawer,
	DrawerClose,
	DrawerContent,
	DrawerDescription,
	DrawerFooter,
	DrawerHeader,
	DrawerTitle,
	DrawerTrigger
} from '@/components/ui/drawer';

export const DashboardSeattingsChangePassword: React.FC = () => {
	return (
		<Drawer>
			<DrawerTrigger className="w-full text-left">
				Change Password
			</DrawerTrigger>
			<DrawerContent className="lg:w-1/2 mx-auto">
				<DrawerHeader>
					<DrawerTitle className="text-4xl">
						Change Password
					</DrawerTitle>
					<DrawerDescription>
						Make sure you write this one down
					</DrawerDescription>
				</DrawerHeader>

				<DrawerFooter className="flex-row">
					<Button className="w-full">Submit</Button>
					<DrawerClose className="w-full">
						<DrawerClose className="inline-flex items-center justify-center whitespace-nowrap rounded-md text-sm font-medium ring-offset-background transition-colors focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring focus-visible:ring-offset-2 disabled:pointer-events-none disabled:opacity-50 border border-input bg-background hover:bg-accent hover:text-accent-foreground h-10 px-4 py-2 w-full">
							Close
						</DrawerClose>
					</DrawerClose>
				</DrawerFooter>
			</DrawerContent>
		</Drawer>
	);
};
