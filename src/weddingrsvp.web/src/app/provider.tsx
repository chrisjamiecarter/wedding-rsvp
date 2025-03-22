import * as React from "react";

import "@mantine/core/styles.css";
import { MantineProvider } from "@mantine/core";

type AppProviderProps = {
  children: React.ReactNode;
};

export function AppProvider({ children }: AppProviderProps) {
  return (
    <MantineProvider>
      <React.Suspense fallback={<div>Loading...</div>}>
        {children}
      </React.Suspense>
    </MantineProvider>
  );
}
