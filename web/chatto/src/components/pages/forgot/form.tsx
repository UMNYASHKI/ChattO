'use client';

import { zodResolver } from '@hookform/resolvers/zod';
import Link from 'next/link';
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
import { Input } from '@/components/ui/input';
import { account } from '@/lib/api/agent';

const formSchema = z.object({
  email: z
    .string()
    .min(2, {
      message: 'Email must be at least 2 characters.'
    })
    .email({
      message: 'Please enter a valid email address.'
    })
});

export const ForgotPasswordForm: React.FC = () => {
  const form = useForm<z.infer<typeof formSchema>>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      email: ''
    }
  });

  // 2. Define a submit handler.
  async function onSubmit(values: z.infer<typeof formSchema>) {
    const res = await account.forgotPassword({
      email: values.email
    });

    if (!res.ok) {
      return;
    }

    // Handle post-submission logic, like displaying a success message
  }

  return (
    <Form {...form}>
      <form
        onSubmit={form.handleSubmit(onSubmit)}
        className="space-y-4 bg-neutral-100 p-12 w-[50rem] rounded-xl flex flex-col"
      >
        <span className="mx-auto text-2xl">FORGOT PASSWORD</span>
        <p className="text-center">
          Enter the email address associated with your account and
          we'll send you a link to reset your password.
        </p>
        <FormField
          control={form.control}
          name="email"
          render={({ field }) => (
            <FormItem>
              <FormControl>
                <Input
                  placeholder="email"
                  {...field}
                  className="rounded-full"
                />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <Button type="submit" className="w-1/2 rounded-full mx-auto">
          SEND LINK
        </Button>
        <div className="mx-auto">
          <span>Don&apos;t have an account? </span>
          <Link
            href="/signup"
            className="underline underline-offset-4"
          >
            Sign up now
          </Link>
        </div>
      </form>
    </Form>
  );
};
