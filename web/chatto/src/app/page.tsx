import React from 'react';

import Footer from '@/components/common/footer';

import DashboardSection from '../components/pages/index/DashboardSection';
import FaqSection from '../components/pages/index/FaqSection';
import MobileSection from '../components/pages/index/MobileSection';
import PricingSection from '../components/pages/index/PricingSection';
import StartSection from '../components/pages/index/StartSection';
import Layout from './layout';

const LandingPage: React.FC = () => {
    return (
        <Layout>
            <StartSection />
            <DashboardSection />
            <MobileSection />
            <PricingSection />
            <FaqSection />
            <Footer />
        </Layout>
    );
};

export default LandingPage;