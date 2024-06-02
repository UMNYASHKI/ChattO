import React from 'react';

import PriceCard from '@/components/common/price-card';

const PricingSection: React.FC = () => {
  return (
    <section className="flex flex-col items-center py-20 w-full space-y-20">
      <h2 className="text-4xl font-bold mb-4">Our Pricing</h2>
      <div className="flex justify-center gap-8 w-full">
        <PriceCard period="3 MONTH" price={100} className="basis-1/3" />
        <PriceCard period="6 MONTH" price={190} className="basis-1/3" />
        <PriceCard period="1 YEAR" price={360} className="basis-1/3" />
      </div>
    </section>
  );
};

export default PricingSection;
