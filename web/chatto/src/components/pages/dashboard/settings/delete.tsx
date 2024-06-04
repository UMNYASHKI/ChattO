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

export const DashboardSeattingsConfirm: React.FC<{
	title: string;
	description: string;
}> = ({ title, description }) => {
	return (
		<Drawer>
			<DrawerTrigger className="w-full text-left">{title}</DrawerTrigger>
			<DrawerContent className="lg:w-1/2 mx-auto">
				<DrawerHeader>
					<DrawerTitle className="text-4xl">
						Are you absolutely sure?
					</DrawerTitle>
					<DrawerDescription>{description}</DrawerDescription>
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
