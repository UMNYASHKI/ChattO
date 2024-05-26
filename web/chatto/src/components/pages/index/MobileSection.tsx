import React from 'react';

const MobileSection: React.FC = () => {
	return (
		<section className="flex flex-col items-center p-10">
			<h2 className="text-2xl font-bold mb-4">Dynamic Participation</h2>
			<img src="/mobile.png" alt="Mobile" className="h-60 w-60" />
			<div className="grid grid-cols-2 gap-6 mt-4 text-gray-700">
				<p>
					Engage in discussions, attach files, and partake in polls
					within your assigned channels.
				</p>
				<p>
					Delete and attach messages seamlessly, tailoring your
					communication for precision.
				</p>
				<p>
					Edit messages with ease using Markdown markup elements for a
					personalized touch.
				</p>
				<p>
					Modify chat or channel details, including photos and names,
					for a personalized touch.
				</p>
				<p>
					Manage notifications by muting message sounds, ensuring a
					disturbance-free environment.
				</p>
				<p>
					Efficiently reach out to all users or selectively tag
					specific participants.
				</p>
			</div>
			<button className="mt-6 px-4 py-2 bg-black text-white rounded">
				Download App
			</button>
		</section>
	);
};

export default MobileSection;
