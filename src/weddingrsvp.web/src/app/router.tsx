import { useMemo } from "react";
import { createBrowserRouter, RouterProvider } from "react-router";
import { QueryClient, useQueryClient } from "@tanstack/react-query";

import { paths } from "@/configs/paths";
import { ProtectedRoute } from "@/lib/auth";
import {
  default as AppRoot,
  ErrorBoundary as AppRootErrorBoundary,
} from "./routes/app/root";

const convert = (queryClient: QueryClient) => (m: any) => {
  const { clientLoader, clientAction, default: Component, ...rest } = m;
  return {
    ...rest,
    loader: clientLoader?.(queryClient),
    action: clientAction?.(queryClient),
    Component,
  };
};

const createAppRouter = (queryClient: QueryClient) => {
  return createBrowserRouter([
    {
      path: paths.home.path,
      lazy: () => import("./routes/landing").then(convert(queryClient)),
    },
    {
      path: paths.auth.signin.path,
      lazy: () => import("./routes/auth/signin").then(convert(queryClient)),
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
          lazy: () =>
            import("./routes/app/dashboard").then(convert(queryClient)),
        },
        {
          path: paths.app.events.path,
          lazy: () =>
            import("./routes/app/events/events").then(convert(queryClient)),
        },
        {
          path: paths.app.event.path,
          lazy: () =>
            import("./routes/app/events/event").then(convert(queryClient)),
        },
        {
          path: paths.app.invites.path,
          lazy: () =>
            import("./routes/app/invites/invites").then(convert(queryClient)),
        },
        {
          path: paths.app.invite.path,
          lazy: () =>
            import("./routes/app/invites/invite").then(convert(queryClient)),
        },
        {
          path: paths.app.guests.path,
          lazy: () =>
            import("./routes/app/guests/guests").then(convert(queryClient)),
        },
        {
          path: paths.app.guest.path,
          lazy: () =>
            import("./routes/app/guests/guest").then(convert(queryClient)),
        },
        {
          path: paths.app.foods.path,
          lazy: () =>
            import("./routes/app/foods/foods").then(convert(queryClient)),
        },
        {
          path: paths.app.food.path,
          lazy: () =>
            import("./routes/app/foods/food").then(convert(queryClient)),
        },
      ],
    },
    {
      path: "*",
      lazy: () => import("./routes/not-found").then(convert(queryClient)),
    },
  ]);
};

export const AppRouter = () => {
  const queryClient = useQueryClient();

  const router = useMemo(() => createAppRouter(queryClient), [queryClient]);

  return <RouterProvider router={router} />;
};
