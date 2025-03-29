export const paths = {
  home: {
    path: "/",
    getHref: () => "/",
  },
  auth: {
    signin: {
      path: "/auth/signin",
      getHref: (redirectTo?: string | null | undefined) =>
        `/auth/signin${
          redirectTo ? `?redirectTo=${encodeURIComponent(redirectTo)}` : ""
        }`,
    },
  },
  app: {
    root: {
      path: "/app",
      getHref: () => "/app",
    },
    dashboard: {
      path: "",
      getHref: () => "/app",
    },
  },
} as const;
