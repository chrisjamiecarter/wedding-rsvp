import { z } from "zod";
import { format } from "date-fns";

import { api } from "@/lib/api-client";
import { Event } from "@/types/api";
import { MutationConfig } from "@/lib/react-query";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { getEventsQueryOptions } from "./get-events";

export const createEventInputSchema = z.object({
  name: z.string().min(1, "Required"),
  description: z.string().default(""),
  venue: z.string().min(1, "Required"),
  address: z.string().min(1, "Required"),
  date: z
    .date()
    .min(new Date(), "Event cannot be in the past")
    .transform((date) => format(date, "yyyy-MM-dd")),
  time: z.string().time(),
  dressCode: z.string().default(""),
});

export type CreateEventInput = z.infer<typeof createEventInputSchema>;

export const createEvent = ({
  data,
}: {
  data: CreateEventInput;
}): Promise<Event> => {
  console.log("EventData", data);
  return api.post("/api/events", data);
};

type UseCreateEventOptions = {
  mutationConfig?: MutationConfig<typeof createEvent>;
};

export const useCreateEvent = ({
  mutationConfig,
}: UseCreateEventOptions = {}) => {
  const queryClient = useQueryClient();

  const { onSuccess, ...restConfig } = mutationConfig || {};

  return useMutation({
    onSuccess: (...args) => {
      queryClient.invalidateQueries({
        queryKey: getEventsQueryOptions().queryKey,
      });
      onSuccess?.(...args);
    },
    ...restConfig,
    mutationFn: createEvent,
  });
};
