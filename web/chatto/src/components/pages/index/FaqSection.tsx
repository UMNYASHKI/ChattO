import React from 'react';

const FaqSection: React.FC = () => {
    return (
        <section className="flex flex-col items-center p-10">
            <h2 className="text-2xl font-bold mb-4">
                Frequently Asked Questions
            </h2>
            <div className="w-full max-w-2xl space-y-4">
                <details className="p-4 bg-gray-100 rounded-md shadow">
                    <summary className="font-bold cursor-pointer">
                        What is the purpose of ChattO?
                    </summary>
                    <p className="mt-2">
                        ChattO is a unified communication hub designed to
                        streamline interactions among diverse organizations.
                    </p>
                </details>
                <details className="p-4 bg-gray-100 rounded-md shadow">
                    <summary className="font-bold cursor-pointer">
                        How often are updates and new features introduced?
                    </summary>
                    <p className="mt-2">
                        Updates and new features are introduced regularly to
                        enhance user experience and functionality.
                    </p>
                </details>
                <details className="p-4 bg-gray-100 rounded-md shadow">
                    <summary className="font-bold cursor-pointer">
                        How long does the free trial period last?
                    </summary>
                    <p className="mt-2">
                        The free trial period lasts for 30 days.
                    </p>
                </details>
                <details className="p-4 bg-gray-100 rounded-md shadow">
                    <summary className="font-bold cursor-pointer">
                        How is user privacy maintained within the platform?
                    </summary>
                    <p className="mt-2">
                        User privacy is maintained through robust security
                        measures and compliance with data protection
                        regulations.
                    </p>
                </details>
                <details className="p-4 bg-gray-100 rounded-md shadow">
                    <summary className="font-bold cursor-pointer">
                        What support resources are available for users and
                        administrators?
                    </summary>
                    <p className="mt-2">
                        Support resources include a comprehensive knowledge
                        base, community forums, and direct support from our
                        team.
                    </p>
                </details>
            </div>
        </section>
    );
};

export default FaqSection;