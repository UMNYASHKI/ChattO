import React from 'react';

const Footer: React.FC = () => {
	return (
		<footer className="bg-black text-white p-10">
			<div className="flex justify-between items-center">
				<div className="flex items-center">
					<img
						src="/logo-white.png"
						alt="ChattO"
						className="h-8 w-8"
					/>
					<span className="ml-2 text-xl font-bold">ChattO</span>
				</div>
				<div className="flex space-x-10">
					<div>
						<h3 className="font-bold">Information</h3>
						<ul>
							<li>About us</li>
							<li>Download app</li>
							<li>Pricing</li>
							<li>FAQ</li>
						</ul>
					</div>
					<div>
						<h3 className="font-bold">Contact us</h3>
						<ul>
							<li>+38(012)-345-67-89</li>
							<li>chatto@gmail.com</li>
						</ul>
					</div>
				</div>
			</div>
		</footer>
	);
};

export default Footer;
