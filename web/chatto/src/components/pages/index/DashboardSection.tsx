import React from 'react';

const DashboardSection: React.FC = () => {
    return (
        <section className="flex flex-col items-center p-10">
            <h2 className="text-2xl font-bold mb-4">
                Control the process of managing users and groups using a
                comprehensive admin panel.
            </h2>
            <img src="/dashboard.png" alt="Dashboard" className="h-60 w-60" />
            <div className="flex flex-col space-y-4 mt-4 text-gray-700">
                <p>Register your organization</p>
                <p>Assign more admins to help you manage the workspace</p>
                <p>Create, edit, delete and manage users</p>
                <p>Manage chats and channels</p>
                <p>
                    View a dashboard that displays the organization`s statistics
                </p>
            </div>
        </section>
    );
};

export default DashboardSection;