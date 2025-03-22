import { createBrowserRouter } from "react-router";
import { RouterProvider } from "react-router/dom";

import { paths } from "@/configs/paths";
import LandingRoute from "./routes/landing";
import NotFound from "./routes/not-found";

export const AppRouter = () => {
  const router = createBrowserRouter([
    {
      path: paths.home.path,
      element: <LandingRoute />,
    },
    {
      path: "*",
      element: <NotFound />,
    },
  ]);

  return <RouterProvider router={router} />;
};
