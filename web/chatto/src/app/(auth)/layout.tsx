import Footer from '@/components/common/footer';
import Header from '@/components/pages/index/Header';

export default function AuthLayout({
	children
}: {
	readonly children: React.ReactNode;
}) {
	return (
		<div className="h-screen flex flex-col">
			<Header />
			<main className="flex flex-1 w-full items-center justify-center">
				{children}
			</main>
			<Footer />
		</div>
	);
}
