import React from 'react';

const StartSection: React.FC = () => {
	return (
		<section className="flex flex-col items-center p-10">
			<img src="/logo-large.png" alt="ChattO" className="h-20 w-20" />
			<h1 className="text-3xl font-bold mt-4">
				Unified Communication Hub for Organizations
			</h1>
			<p className="text-center mt-4 text-gray-700">
				Experience seamless communication with Chatto, an all-in-one
				platform designed to streamline interactions among diverse
				organizations.
			</p>
			<button className="mt-6 px-4 py-2 bg-black text-white rounded">
				Get started
			</button>
		</section>
	);
};

export default StartSection;
