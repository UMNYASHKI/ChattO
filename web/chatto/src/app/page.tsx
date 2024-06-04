import React from 'react';

import Footer from '@/components/common/footer';
import Header from '@/components/pages/index/Header';

import DashboardSection from '../components/pages/index/DashboardSection';
import FaqSection from '../components/pages/index/FaqSection';
import MobileSection from '../components/pages/index/MobileSection';
import PricingSection from '../components/pages/index/PricingSection';
import StartSection from '../components/pages/index/StartSection';

const LandingPage: React.FC = () => {
	return (
		<>
			<Header />
			<main className="px-64">
				<StartSection />
				<DashboardSection />
				<MobileSection />
				<PricingSection />
				<FaqSection />
			</main>
			<Footer />
		</>
	);
};

export default LandingPage;
