import { useState } from 'react';

import { RegistrationStep1Form } from '@/components/pages/signup/step1';
import { RegistrationStep2Form } from '@/components/pages/signup/step2';

const Registration: React.FC = () => {
  const [step, setStep] = useState(1);
  const [formData, setFormData] = useState({});

  const handleNext = (data) => {
    setFormData({ ...formData, ...data });
    setStep(step + 1);
  };

  return (
    <main className="flex flex-1 w-full items-center justify-center">
      {step === 1 && <RegistrationStep1Form onNext={handleNext} />}
      {step === 2 && <RegistrationStep2Form onNext={handleNext} />}
    </main>
  );
};

export default Registration;
