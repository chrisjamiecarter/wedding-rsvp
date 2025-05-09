import * as React from "react";
import { ErrorBoundary } from "react-error-boundary";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";

import { MantineProvider } from "@mantine/core";
import { Notifications } from "@mantine/notifications";

import { AuthLoader } from "@/lib/auth";
import { queryConfig } from "@/lib/react-query";
import { MainErrorFallback } from "@/components/errors/main";
import { LoadingPage } from "@/components/ui/loading-page";

type AppProviderProps = {
  children: React.ReactNode;
};

export const AppProvider = ({ children }: AppProviderProps) => {
  const [queryClient] = React.useState<QueryClient>(
    () => new QueryClient({ defaultOptions: queryConfig })
  );

  return (
    <MantineProvider>
      <React.Suspense fallback={<LoadingPage />}>
        <ErrorBoundary FallbackComponent={MainErrorFallback}>
          <QueryClientProvider client={queryClient}>
            <Notifications position="top-right" />
            <AuthLoader renderLoading={() => <LoadingPage />}>
              {children}
            </AuthLoader>
          </QueryClientProvider>
        </ErrorBoundary>
      </React.Suspense>
    </MantineProvider>
  );
};
