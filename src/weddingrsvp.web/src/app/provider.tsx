import * as React from "react";

import "@mantine/core/styles.css";
import { MantineProvider } from "@mantine/core";
import { AuthProvider } from "@/lib/auth";

type AppProviderProps = {
  children: React.ReactNode;
};

export function AppProvider({ children }: AppProviderProps) {
  return (
    <MantineProvider>
      <React.Suspense fallback={<div>Loading...</div>}>
      <AuthProvider>
        {children}
      </AuthProvider>
      </React.Suspense>
    </MantineProvider>
  );
}
