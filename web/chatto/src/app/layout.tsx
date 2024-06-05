import './styles/globals.css';

import clsx from 'clsx';
import { Mitr } from 'next/font/google';
import React from 'react';

import { Toaster } from '@/components/ui/toaster';

const font = Mitr({
	subsets: ['latin'],
	weight: ['300', '400', '500', '700'],
	display: 'swap',
	variable: '--font-mitr'
});

export default function RootLayout({
	children
}: Readonly<{ children: React.ReactNode }>) {
	return (
		<html className="scroll-smooth">
			<head>
				<title>ChattO</title>
				<link rel="icon" href="/favicon.ico" />
			</head>
			<body className={clsx(font.variable, 'font-mitr')}>
				{children}
				<Toaster />
			</body>
		</html>
	);
}
