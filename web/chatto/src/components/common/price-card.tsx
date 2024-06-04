import clsx from 'clsx';
import React from 'react';

const PriceCard: React.FC<{
	period: string;
	price: number;
	className?: string;
}> = ({ period, price, className }) => {
	return (
		<div
			className={clsx(
				'flex flex-col justify-between item-center p-6 bg-gray-100 rounded-lg shadow-md',
				className
			)}
		>
			<h2 className="text-xl font-semibold mb-2">{period}</h2>
			<p className="text-4xl font-bold mb-2">${price}</p>
			<p className="text-gray-500 mb-4">PER ORGANIZATION</p>
			<button className="px-4 py-2 bg-black text-white rounded">
				GET STARTED
			</button>
		</div>
	);
};

export default PriceCard;
