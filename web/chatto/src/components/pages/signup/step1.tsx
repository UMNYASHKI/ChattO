'use client';

import { zodResolver } from '@hookform/resolvers/zod';
import { useForm } from 'react-hook-form';
import { z } from 'zod';

import { Button } from '@/components/ui/button';
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage
} from '@/components/ui/form';
import { Input, Select, SelectItem } from '@/components/ui/input';

const formSchema = z.object({
  organizationName: z.string().min(1, {
    message: 'Organization Name is required.'
  }),
  organizationType: z.string().min(1, {
    message: 'Organization Type is required.'
  })
});

export const RegistrationStep1Form: React.FC = ({ onNext }) => {
  const form = useForm<z.infer<typeof formSchema>>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      organizationName: '',
      organizationType: ''
    }
  });

  function onSubmit(values: z.infer<typeof formSchema>) {
    onNext(values);
  }

  return (
    <Form {...form}>
      <form
        onSubmit={form.handleSubmit(onSubmit)}
        className="space-y-4 bg-neutral-100 p-12 w-[50rem] rounded-xl flex flex-col"
      >
        <span className="mx-auto text-2xl">REGISTRATION 1/3</span>
        <FormField
          control={form.control}
          name="organizationName"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Organization Name</FormLabel>
              <FormControl>
                <Input
                  placeholder="Organization Name"
                  {...field}
                  className="rounded-full"
                />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="organizationType"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Organization Type</FormLabel>
              <FormControl>
                <Select
                  placeholder="Select an organization type"
                  {...field}
                  className="rounded-full"
                >
                  <SelectItem value="type1">
                    Type 1
                  </SelectItem>
                  <SelectItem value="type2">
                    Type 2
                  </SelectItem>
                </Select>
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <Button type="submit" className="w-1/2 rounded-full mx-auto">
          NEXT
        </Button>
      </form>
    </Form>
  );
};
