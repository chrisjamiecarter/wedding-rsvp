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
    dashboard: {
      path: "/app",
      getHref: () => "/app",
    },
  },
} as const;
