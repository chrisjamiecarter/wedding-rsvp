import { queryOptions, useQuery } from "@tanstack/react-query";

import { api } from "@/lib/api-client";
import { QueryConfig } from "@/lib/react-query";
import { InviteRsvp } from "@/types/api";

export type GetRvspProps = {
  inviteId: string;
  token: string;
};

export const getRsvp = ({
  inviteId,
  token,
}: GetRvspProps): Promise<InviteRsvp> => {
  return api.get(`/api/rsvps/${inviteId}?token=${token}`);
};

export const getRsvpQueryOptions = (inviteId: string, token: string) => {
  return queryOptions({
    queryKey: ["rvsps", { inviteId, token }],
    queryFn: () => getRsvp({ inviteId, token }),
  });
};

interface UseRsvpOptions {
  inviteId: string;
  token: string;
  queryConfig?: QueryConfig<typeof getRsvpQueryOptions>;
}

export const useRsvp = ({ inviteId, token, queryConfig }: UseRsvpOptions) => {
  return useQuery({
    ...getRsvpQueryOptions(inviteId, token),
    ...queryConfig,
  });
};
