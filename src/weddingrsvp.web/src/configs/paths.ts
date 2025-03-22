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
} as const;
