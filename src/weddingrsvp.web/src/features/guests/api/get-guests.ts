import { api } from "@/lib/api-client";
import { QueryConfig } from "@/lib/react-query";
import { GuestDetail, PagedResponse } from "@/types/api";
import { queryOptions, useQuery } from "@tanstack/react-query";

export const getGuests = (
  inviteId: string,
  pageNumber = 1
): Promise<PagedResponse<GuestDetail>> => {
  return api.get(`/api/invites/${inviteId}/guests`, {
    params: {
      pageNumber,
    },
  });
};

export const getGuestsQueryOptions = ({
  inviteId,
  pageNumber = 1,
}: {
  inviteId: string;
  pageNumber?: number;
}) => {
  return queryOptions({
    queryKey: ["guests", { inviteId, pageNumber }],
    queryFn: () => getGuests(inviteId, pageNumber),
  });
};

type UseGuestsOptions = {
  inviteId: string;
  pageNumber?: number;
  queryConfig?: QueryConfig<typeof getGuests>;
};

export const useGuests = ({
  inviteId,
  pageNumber,
  queryConfig,
}: UseGuestsOptions) => {
  return useQuery({
    ...getGuestsQueryOptions({ inviteId, pageNumber }),
    ...queryConfig,
  });
};
