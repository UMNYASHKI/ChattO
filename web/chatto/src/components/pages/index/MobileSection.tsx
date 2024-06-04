import Image from 'next/image';
import React from 'react';

import Mobile from '@/../public/mobile.png';

const MobileSection: React.FC = () => {
	return (
		<section
			id="download"
			className="flex flex-col items-center justify-center h-screen"
		>
			<div className="flex flex-col md:flex-row justify-center items-center w-full">
				<div className="flex flex-col w-full md:w-1/3 md:px-10 text-center space-y-16 *:space-y-8">
					<div>
						<h2 className="text-xl font-medium">
							Dynamic Participation
						</h2>
						<p>
							Engage in discussions, attach files, and partake in
							polls within your assigned channels.
						</p>
					</div>
					<div>
						<h2 className="text-xl font-medium">
							Markdown Messaging
						</h2>
						<p>
							Edit messages with ease using Markdown markup
							elements for a personalized touch.
						</p>
					</div>
					<div>
						<h2 className="text-xl font-medium">
							Notification Control
						</h2>
						<p>
							Manage notifications by muting message sounds,
							ensuring a disturbance-free environment.
						</p>
					</div>
				</div>
				<div className="flex flex-col items-center w-full h-full md:w-1/3 px-4 basis-1/2">
					<Image
						alt="Mobile"
						src={Mobile}
						className="h-full object-contain"
					/>
					<button className="mt-6 px-4 py-2 w-fit bg-black text-white rounded-full">
						Download App
					</button>
				</div>
				<div className="flex flex-col w-full md:w-1/3 md:px-10 text-center space-y-16 *:space-y-8">
					<div>
						<h2 className="text-xl font-medium">
							Message Manipulation
						</h2>
						<p>
							Delete and attach messages seamlessly, tailoring
							your communication for precision.
						</p>
					</div>
					<div>
						<h2 className="text-xl font-medium">
							Chat Customization
						</h2>
						<p>
							Modify chat or channel details, including photos and
							names, for a personalized touch.
						</p>
					</div>
					<div>
						<h2 className="text-xl font-medium">
							Tagging Capabilities
						</h2>
						<p>
							Efficiently reach out to all users or selectively
							tag specific participants.
						</p>
					</div>
				</div>
			</div>
		</section>
	);
};

export default MobileSection;
