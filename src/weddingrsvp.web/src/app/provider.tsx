import * as React from "react";
import { ErrorBoundary } from "react-error-boundary";

import { MainErrorFallback } from "@/components/errors/main";
import { AuthProvider } from "@/lib/auth";

import "@mantine/core/styles.css";
import { Center, MantineProvider } from "@mantine/core";
import { Loader } from "@/components/ui/loader";

type AppProviderProps = {
  children: React.ReactNode;
};

export const AppProvider = ({ children }: AppProviderProps) => {
  return (
    <MantineProvider>
      <React.Suspense
        fallback={
          <Center>
            <Loader size="xl" />
          </Center>
        }>
        <ErrorBoundary FallbackComponent={MainErrorFallback}>
          {/* <AuthLoader renderLoading={() => (<Center><Loader size="xl" /></Center>)}></AuthLoader> */}
          <AuthProvider>{children}</AuthProvider>
        </ErrorBoundary>
      </React.Suspense>
    </MantineProvider>
  );
};
