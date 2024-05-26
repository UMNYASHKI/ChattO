import type { Config } from 'tailwindcss';

const config: Config = {
	content: [
		'./src/pages/**/*.{js,ts,jsx,tsx,mdx}',
		'./src/components/**/*.{js,ts,jsx,tsx,mdx}',
		'./src/app/**/*.{js,ts,jsx,tsx,mdx}'
	],
	theme: {
		extend: {
			backgroundImage: {
				'gradient-radial': 'radial-gradient(var(--tw-gradient-stops))',
				'gradient-conic':
					'conic-gradient(from 180deg at 50% 50%, var(--tw-gradient-stops))'
			},
			fontFamily: {
				cinzel: ['var(--font-cinzel)'],
				poppins: ['var(--font-poppins)']
			},
			colors: {
				transparent: 'transparent',
				foreground: 'hsl(var(--foreground) / <alpha-value>)',
				background: 'hsl(var(--background) / <alpha-value>)',
				accent: 'hsl(var(--accent) / <alpha-value>)'
			}
		}
	},
	plugins: []
};
export default config;
