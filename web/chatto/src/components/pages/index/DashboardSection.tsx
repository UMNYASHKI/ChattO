import Image from 'next/image';
import React from 'react';

import Bubbles from '@/../public/bubbles.png';
import Dashboard from '@/../public/dashboard.png';

const DashboardSection: React.FC = () => {
  return (
    <section className="flex flex-col h-screen items-center justify-center text-xl">
      <h1 className="text-4xl">
        Control the process of managing users and <br />
        groups using a comprehensive admin panel.
      </h1>
      <div className="flex flex-row w-full">
        <Image
          alt="Dashboard"
          src={Dashboard}
          className="w-3/5 h-full object-contain"
        />
        <Image
          alt="Bubbles"
          src={Bubbles}
          className="w-2/5 h-full object-contain"
        />
      </div>
    </section>
  );
};

export default DashboardSection;
