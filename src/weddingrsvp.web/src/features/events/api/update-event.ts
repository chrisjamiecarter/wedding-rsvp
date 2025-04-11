import { useMutation, useQueryClient } from "@tanstack/react-query";
import { format } from "date-fns";
import { z } from "zod";

import { api } from "@/lib/api-client";
import { Event } from "@/types/api";
import { MutationConfig } from "@/lib/react-query";

import { getEventQueryOptions } from "./get-event";

export const updateEventInputSchema = z.object({
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

export type UpdateEventInput = z.infer<typeof updateEventInputSchema>;

export const updateEvent = ({
  data,
  eventId,
}: {
  data: UpdateEventInput;
  eventId: string;
}): Promise<Event> => {
  console.log("EventData", data);
  return api.put(`/api/events/${eventId}`, data);
};

type UseUpdateEventOptions = {
  mutationConfig?: MutationConfig<typeof updateEvent>;
};

export const useUpdateEvent = ({
  mutationConfig,
}: UseUpdateEventOptions = {}) => {
  const queryClient = useQueryClient();

  const { onSuccess, ...restConfig } = mutationConfig || {};

  return useMutation({
    onSuccess: (data, ...args) => {
      queryClient.refetchQueries({
        queryKey: getEventQueryOptions(data.id).queryKey,
      });
      onSuccess?.(data, ...args);
    },
    ...restConfig,
    mutationFn: updateEvent,
  });
};
