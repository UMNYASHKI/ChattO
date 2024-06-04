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
						<Button variant="outline" className="w-full">
							Cancel
						</Button>
					</DrawerClose>
				</DrawerFooter>
			</DrawerContent>
		</Drawer>
	);
};
