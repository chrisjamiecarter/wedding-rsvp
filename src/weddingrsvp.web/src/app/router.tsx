import { useMemo } from "react";
import { createBrowserRouter, RouterProvider } from "react-router";

import { paths } from "@/configs/paths";
import { ProtectedRoute } from "@/lib/auth";
import {
  default as AppRoot,
  ErrorBoundary as AppRootErrorBoundary,
} from "./routes/app/root";

const convert = (m: any) => {
  const { clientLoader, clientAction, default: Component, ...rest } = m;
  return {
    ...rest,
    loader: clientLoader?.(),
    action: clientAction?.(),
    Component,
  };
};

export const createAppRouter = () => {
  return createBrowserRouter([
    {
      path: paths.home.path,
      lazy: () => import("./routes/landing").then(convert),
    },
    {
      path: paths.auth.signin.path,
      lazy: () => import("./routes/auth/signin").then(convert),
    },
    {
      path: paths.app.root.path,
      element: (
        <ProtectedRoute>
          <AppRoot />
        </ProtectedRoute>
      ),
      ErrorBoundary: AppRootErrorBoundary,
      children: [
        {
          path: paths.app.dashboard.path,
          lazy: () => import("./routes/app/dashboard").then(convert),
        },
      ],
    },
    {
      path: "*",
      lazy: () => import("./routes/not-found").then(convert),
    },
  ]);
};

export const AppRouter = () => {
  const router = useMemo(() => createAppRouter(), []);

  return <RouterProvider router={router} />;
};
