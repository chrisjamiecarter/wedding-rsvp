import { api } from "@/lib/api-client";
import { QueryConfig } from "@/lib/react-query";
import { Food, PagedResponse } from "@/types/api";
import { queryOptions, useQuery } from "@tanstack/react-query";

export const getFoods = (
  eventId: string,
  pageNumber = 1
): Promise<PagedResponse<Food>> => {
  return api.get(`/api/events/${eventId}/foodoptions`, {
    params: {
      pageNumber,
    },
  });
};

export const getFoodsQueryOptions = ({
  eventId,
  pageNumber = 1,
}: {
  eventId: string;
  pageNumber?: number;
}) => {
  return queryOptions({
    queryKey: ["foods", { eventId, pageNumber }],
    queryFn: () => getFoods(eventId, pageNumber),
  });
};

type UseFoodsOptions = {
  eventId: string;
  pageNumber?: number;
  queryConfig?: QueryConfig<typeof getFoods>;
};

export const useFoods = ({
  eventId,
  pageNumber,
  queryConfig,
}: UseFoodsOptions) => {
  return useQuery({
    ...getFoodsQueryOptions({ eventId, pageNumber }),
    ...queryConfig,
  });
};
