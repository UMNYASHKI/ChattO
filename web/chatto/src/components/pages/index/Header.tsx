import React from 'react';

const Header: React.FC = () => {
	return (
		<header className="flex justify-between items-center p-6">
			<div className="flex items-center">
				<img src="/logo.png" alt="ChattO" className="h-8 w-8" />
				<span className="ml-2 text-xl font-bold">ChattO</span>
			</div>
			<nav className="flex space-x-6">
				<a href="#about" className="text-gray-700">
					About us
				</a>
				<a href="#download" className="text-gray-700">
					Download app
				</a>
				<a href="#pricing" className="text-gray-700">
					Pricing
				</a>
				<a href="#faq" className="text-gray-700">
					FAQ
				</a>
				<a href="#login" className="text-gray-700">
					Log in
				</a>
			</nav>
		</header>
	);
};

export default Header;
