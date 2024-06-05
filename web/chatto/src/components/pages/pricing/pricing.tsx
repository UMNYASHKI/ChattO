'use client'; // Add this line at the top

import { getSession } from '@/lib/actions/session';
import React, { useState, useEffect } from 'react';
import { useRouter } from 'next/navigation';

const Pricing: React.FC = () => {
  const [billingId, setBillingId] = useState('');
  const router = useRouter();

  async function getToken() {
    const session = await getSession();
    return session.token;
  }

  const createOrder = async () => {
    const token = await getToken();
    return fetch("https://localhost:7195/api/Billing?billingInfoId=b92d6bb1-d293-4de3-b6ac-d6b33795e396", {
      method: "post",
      headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + token
      }
    }).then(async (response) => {
      if (!response.ok) {
        return response.json().then(error => { throw error; });
      }
      const result = await response.json();
      setBillingId(result.billingId);
      return result;
    }).then((order) => order.id)
      .catch(error => console.log(error.message));
  };

  const onApprove = async () => {
    const token = await getToken();
    return fetch("https://chatto.cloud/api/Billing?billingId=" + billingId, {
      method: "put",
      headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + token
      }
    }).then((response) => {
      if (!response.ok) {
        return response.json().then(error => { throw error; });
      }
      console.log("success");
      router.push('/dashboard'); // client-side navigation

    }).catch(error => console.log(error.message));
  };

  useEffect(() => {
    const script = document.createElement('script');
    script.src = "https://www.paypal.com/sdk/js?client-id=AaFC1lng6Ns1DYwD5-gbeF0zlYGNnEq6cpVTq_timm29SuNYTtoUx1p3ZIBUdUfG4tYVPFNs5UcVd6nW";
    script.addEventListener('load', () => {
      // @ts-ignore
      paypal.Buttons({
        style: {
          layout: 'vertical',
          color: 'silver',
          tagline: 'false'
        },
        createOrder: createOrder,
        onApprove: onApprove
      }).render('#paypal-button-container');
    });
    document.body.appendChild(script);

    return () => {
      document.body.removeChild(script);
    };
  }, [billingId]);

  return (
    <div className="text-center">
      <div id="paypal-button-container"></div>
    </div>
  );
};

export default Pricing;
