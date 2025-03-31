import { queryOptions, useQuery } from "@tanstack/react-query";

import { api } from "@/lib/api-client";
import { QueryConfig } from "@/lib/react-query";
import { Event, PagedResponse } from "@/types/api";

export const getEvents = (page = 1): Promise<PagedResponse<Event>> => {
  return api.get(`/api/events`, {
    params: {
      page,
    },
  });
};

export const getEventsQueryOptions = ({ page }: { page?: number } = {}) => {
  return queryOptions({
    queryKey: page ? ["events", { page }] : ["events"],
    queryFn: () => getEvents(page),
  });
};

interface UseEventsOptions {
  page?: number;
  queryConfig?: QueryConfig<typeof getEventsQueryOptions>;
}

export const useEvents = ({ queryConfig, page }: UseEventsOptions) => {
  return useQuery({
    ...getEventsQueryOptions({ page }),
    ...queryConfig,
  });
};
