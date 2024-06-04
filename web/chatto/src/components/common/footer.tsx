import Link from 'next/link';
import React from 'react';

import Logo from './logo';

const footer: React.FC = () => {
	return (
		<footer className="flex justify-between w-full bg-black text-white p-10">
			<Link href="/" className="flex items-center space-x-4">
				<Logo className="h-12 w-12 fill-white" />
				<span>ChattO</span>
			</Link>
			<div className="flex space-x-20">
				<div className="uppercase space-y-2">
					<h3 className="font-medium">Information</h3>
					<ul className="space-y-1">
						<li>
							<Link href="/#about">About us</Link>
						</li>
						<li>
							<Link href="/#download">Download app</Link>
						</li>
						<li>
							<Link href="/#pricing">Pricing</Link>
						</li>
						<li>
							<Link href="/#faq">FAQ</Link>
						</li>
					</ul>
				</div>
				<div className="space-y-2">
					<h3 className="font-medium uppercase">Contact us</h3>
					<ul className="space-y-1">
						<li>
							<Link href="tel:+38(012)-345-67-89">
								+38(012)-345-67-89
							</Link>
						</li>
						<li>
							<Link href="mailto:chatto@gmail.com">
								chatto@gmail.com
							</Link>
						</li>
					</ul>
				</div>
			</div>
		</footer>
	);
};

export default footer;
