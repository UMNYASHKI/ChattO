import React from 'react';

const PricingSection: React.FC = () => {
    return (
        <section className="flex flex-col items-center p-10">
            <h2 className="text-2xl font-bold mb-4">Our Pricing</h2>
            <div className="flex space-x-10">
                <div className="bg-gray-100 p-6 rounded-lg shadow-md text-center">
                    <h3 className="text-xl font-bold">3 Month</h3>
                    <p className="text-2xl font-bold my-4">$100</p>
                    <p>per organization</p>
                    <button className="mt-4 px-4 py-2 bg-black text-white rounded">
                        Get started
                    </button>
                </div>
                <div className="bg-gray-100 p-6 rounded-lg shadow-md text-center">
                    <h3 className="text-xl font-bold">6 Months</h3>
                    <p className="text-2xl font-bold my-4">$190</p>
                    <p>per organization</p>
                    <button className="mt-4 px-4 py-2 bg-black text-white rounded">
                        Get started
                    </button>
                </div>
                <div className="bg-gray-100 p-6 rounded-lg shadow-md text-center">
                    <h3 className="text-xl font-bold">1 Year</h3>
                    <p className="text-2xl font-bold my-4">$360</p>
                    <p>per organization</p>
                    <button className="mt-4 px-4 py-2 bg-black text-white rounded">
                        Get started
                    </button>
                </div>
            </div>
        </section>
    );
};

export default PricingSection;