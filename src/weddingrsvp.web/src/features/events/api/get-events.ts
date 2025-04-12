import { queryOptions, useQuery } from "@tanstack/react-query";

import { api } from "@/lib/api-client";
import { QueryConfig } from "@/lib/react-query";
import { Event, PagedResponse } from "@/types/api";

export const getEvents = (pageNumber = 1): Promise<PagedResponse<Event>> => {
  return api.get(`/api/events`, {
    params: {
      pageNumber,
    },
  });
};

export const getEventsQueryOptions = ({
  pageNumber,
}: { pageNumber?: number } = {}) => {
  return queryOptions({
    queryKey: pageNumber ? ["events", { pageNumber }] : ["events"],
    queryFn: () => getEvents(pageNumber),
  });
};

interface UseEventsOptions {
  pageNumber?: number;
  queryConfig?: QueryConfig<typeof getEventsQueryOptions>;
}

export const useEvents = ({ queryConfig, pageNumber }: UseEventsOptions) => {
  return useQuery({
    ...getEventsQueryOptions({ pageNumber }),
    ...queryConfig,
  });
};
