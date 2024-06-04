import React from 'react';

import {
	Accordion,
	AccordionContent,
	AccordionItem,
	AccordionTrigger
} from '@/components/ui/accordion';

const FaqSection: React.FC = () => {
	return (
		<section
			id="faq"
			className="flex flex-col items-center py-20 space-y-20"
		>
			<h2 className="text-4xl font-bold mb-4">
				Frequently Asked Questions
			</h2>
			<div className="w-full space-y-4">
				<Accordion type="single" collapsible>
					<AccordionItem value="item-1">
						<AccordionTrigger>
							What is the purpose of ChattO?
						</AccordionTrigger>
						<AccordionContent>
							ChattO is a unified communication hub designed to
							streamline interactions among diverse organizations.
						</AccordionContent>
					</AccordionItem>
					<AccordionItem value="item-2">
						<AccordionTrigger>
							How often are updates and new features introduced?
						</AccordionTrigger>
						<AccordionContent>
							Updates and new features are introduced regularly to
							enhance user experience and functionality.
						</AccordionContent>
					</AccordionItem>
					<AccordionItem value="item-3">
						<AccordionTrigger>
							How long does the free trial period last?
						</AccordionTrigger>
						<AccordionContent>
							The free trial period lasts for 30 days.
						</AccordionContent>
					</AccordionItem>
					<AccordionItem value="item-4">
						<AccordionTrigger>
							How is user privacy maintained within the platform?
						</AccordionTrigger>
						<AccordionContent>
							User privacy is maintained through robust security
							measures and compliance with data protection
							regulations.
						</AccordionContent>
					</AccordionItem>
					<AccordionItem value="item-5">
						<AccordionTrigger>
							What support resources are available for users and
							administrators?
						</AccordionTrigger>
						<AccordionContent>
							Support resources include a comprehensive knowledge
							base, community forums, and direct support from our
							team.
						</AccordionContent>
					</AccordionItem>
				</Accordion>
			</div>
		</section>
	);
};

export default FaqSection;
