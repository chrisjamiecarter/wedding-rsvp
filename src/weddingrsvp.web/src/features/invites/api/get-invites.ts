import { api } from "@/lib/api-client";
import { QueryConfig } from "@/lib/react-query";
import { Invite, PagedResponse } from "@/types/api";
import { queryOptions, useQuery } from "@tanstack/react-query";

export const getInvites = (
  eventId: string,
  pageNumber = 1
): Promise<PagedResponse<Invite>> => {
  return api.get(`/api/events/${eventId}/invites`, {
    params: {
      pageNumber,
    },
  });
};

export const getInvitesQueryOptions = ({
  eventId,
  pageNumber = 1,
}: {
  eventId: string;
  pageNumber?: number;
}) => {
  return queryOptions({
    queryKey: ["invites", { eventId, pageNumber }],
    queryFn: () => getInvites(eventId, pageNumber),
  });
};

type UseInvitesOptions = {
  eventId: string;
  pageNumber?: number;
  queryConfig?: QueryConfig<typeof getInvites>;
};

export const useInvites = ({
  eventId,
  pageNumber,
  queryConfig,
}: UseInvitesOptions) => {
  return useQuery({
    ...getInvitesQueryOptions({ eventId, pageNumber }),
    ...queryConfig,
  });
};
