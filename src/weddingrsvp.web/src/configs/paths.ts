export const paths = {
  home: {
    path: "/",
    getHref: () => "/",
  },
  rsvp: {
    path: "/rsvp",
    getHref: () => "/rsvp",
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
    events: {
      path: "events",
      getHref: () => "/app/events",
    },
    event: {
      path: "events/:eventId",
      getHref: (id: string) => `/app/events/${id}`,
    },
    invites: {
      path: "invites",
      getHref: () => "/app/invites",
    },
    invite: {
      path: "invites/:inviteId",
      getHref: (id: string) => `/app/invites/${id}`,
    },
    guests: {
      path: "guests",
      getHref: () => "/app/guests",
    },
    guest: {
      path: "guests/:guestId",
      getHref: (id: string) => `/app/guests/${id}`,
    },
    foods: {
      path: "foods",
      getHref: () => "/app/foods",
    },
    food: {
      path: "foods/:foodId",
      getHref: (id: string) => `/app/foods/${id}`,
    },
  },
} as const;
