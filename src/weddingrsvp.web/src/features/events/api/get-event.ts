import { queryOptions, useQuery } from "@tanstack/react-query";

import { api } from "@/lib/api-client";
import { QueryConfig } from "@/lib/react-query";
import { Event } from "@/types/api";

export const getEvent = ({ eventId }: { eventId: string }): Promise<Event> => {
  return api.get(`/api/events/${eventId}`);
};

export const getEventQueryOptions = (eventId: string) => {
  return queryOptions({
    queryKey: ["events", eventId],
    queryFn: () => getEvent({ eventId }),
  });
};

interface UseEventOptions {
  eventId: string;
  queryConfig?: QueryConfig<typeof getEventQueryOptions>;
}

export const useEvent = ({ eventId, queryConfig }: UseEventOptions) => {
  return useQuery({
    ...getEventQueryOptions(eventId),
    ...queryConfig,
  });
};
