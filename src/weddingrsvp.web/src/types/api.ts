export type BaseEntity = {
  id: string;
};

export type Entity<T> = {
  [K in keyof T]: T[K];
} & BaseEntity;

export type User = Entity<{
  email: string;
  isAdmin: boolean;
}>;

export type PagedResponse<T> = {
  items: T[];
  pageNumber: number;
  pageSize: number;
  totalCount: number;
  totalPages: number;
  hasNextPage: boolean;
  hasPreviousPage: boolean;
  isFirstPage: boolean;
  isLastPage: boolean;
};

export type Event = Entity<{
  name: string;
  description: string;
  venue: string;
  address: string;
  date: Date;
  time: string;
  dressCode: string;
}>;

export type Invite = Entity<{
  eventId: string;
  email: string;
  householdName: string;
  token: string;
}>;

export type Food = Entity<{
  eventId: string;
  name: string;
  foodType: string;
}>;

export const FoodTypes = ["Main", "Dessert"] as const;

export type Guest = Entity<{
  inviteId: string;
  name: string;
  rsvpStatus: string;
  mainFoodOptionId: string | null;
  dessertFoodOptionId: string | null;
}>;

export type GuestDetail = Entity<{
  inviteId: string;
  inviteHouseholdName: string;
  name: string;
  rsvpStatus: string;
  mainFoodOptionId: string | null;
  mainFoodOptionName: string | null;
  dessertFoodOptionId: string | null;
  dessertFoodOptionName: string | null;
}>;

export type InviteRsvp = {
  token: string;
  guests: GuestRsvp[];
  mains: FoodRsvp[];
  desserts: FoodRsvp[];
};

export const RsvpStatuses = ["Attending", "NotAttending"] as const;

export const RsvpStatusOptions = [
  { value: "Attending", label: "Attending" },
  { value: "NotAttending", label: "Not Attending" },
];

export type GuestRsvp = {
  guestId: string;
  name: string;
};

export type FoodRsvp = {
  foodOptionId: string;
  name: string;
};
