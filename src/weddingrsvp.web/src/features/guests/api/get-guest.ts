import { queryOptions, useQuery } from "@tanstack/react-query";

import { api } from "@/lib/api-client";
import { QueryConfig } from "@/lib/react-query";
import { Guest } from "@/types/api";

export const getGuest = ({ guestId }: { guestId: string }): Promise<Guest> => {
  return api.get(`/api/guests/${guestId}`);
};

export const getGuestQueryOptions = (guestId: string) => {
  return queryOptions({
    queryKey: ["guests", guestId],
    queryFn: () => getGuest({ guestId }),
  });
};

interface UseGuestOptions {
  guestId: string;
  queryConfig?: QueryConfig<typeof getGuestQueryOptions>;
}

export const useGuest = ({ guestId, queryConfig }: UseGuestOptions) => {
  return useQuery({
    ...getGuestQueryOptions(guestId),
    ...queryConfig,
  });
};
