import './styles/globals.css';

import React from 'react';

import Footer from '../components/common/footer';
import Header from '../components/pages/index/Header';

export default function RootLayout({
    children
}: Readonly<{ children: React.ReactNode }>) {
    return (
        <html>
            <head>
                <title>ChattO</title>
                <link rel="icon" href="/favicon.ico" />
            </head>
            <body className="font-poppins">
                <Header />
                <main>{children}</main>
            </body>
        </html>
    );
}