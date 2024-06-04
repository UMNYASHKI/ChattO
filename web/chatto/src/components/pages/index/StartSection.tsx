import Image from 'next/image';
import React from 'react';

import Start from '@/../public/start.png';
import Logo from '@/components/common/logo';

const StartSection: React.FC = () => {
	return (
		<section className="flex h-screen items-center justify-center text-xl">
			<div className="w-3/5 space-y-6">
				<div className="flex text-6xl font-bold space-x-4 items-baseline">
					<Logo className="h-20 w-20 fill-white bg-gradient-to-tr from-black to-black/70 p-4 rounded-2xl" />
					<span>Chatto</span>
				</div>
				<h1 className="text-4xl">
					Unified Communication Hub
					<br /> for Organizations
				</h1>
				<p className="w-3/4">
					Experience seamless communication with Chatto, an all-in-one
					platform designed to streamline interactions among diverse
					organizations.
				</p>
				<button className="mt-6 px-4 py-2 bg-black text-white rounded">
					Get started
				</button>
			</div>
			<div className="w-2/5">
				<Image alt="x" src={Start} className="h-full object-contain" />
			</div>
		</section>
	);
};

export default StartSection;
