import Link from 'next/link';
import React from 'react';

import Logo from '@/components/common/logo';

const Header: React.FC = () => {
  return (
    <header className="w-full flex justify-between items-center p-6 text-xl">
      <Link href="/" className="flex items-center space-x-4">
        <Logo className="h-8 w-8 fill-black" />
        <span>ChattO</span>
      </Link>
      <nav className="flex space-x-6 ">
        <a href="/#about">About us</a>
        <a href="/#download">Download app</a>
        <a href="/#pricing">Pricing</a>
        <a href="/#faq">FAQ</a>
      </nav>
      <Link href="/signin">Log in</Link>
    </header>
  );
};

export default Header;
