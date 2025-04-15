import { queryOptions, useQuery } from "@tanstack/react-query";

import { api } from "@/lib/api-client";
import { QueryConfig } from "@/lib/react-query";
import { Invite } from "@/types/api";

export const getInvite = ({
  inviteId,
}: {
  inviteId: string;
}): Promise<Invite> => {
  return api.get(`/api/invites/${inviteId}`);
};

export const getInviteQueryOptions = (inviteId: string) => {
  return queryOptions({
    queryKey: ["invites", inviteId],
    queryFn: () => getInvite({ inviteId }),
  });
};

interface UseInviteOptions {
  inviteId: string;
  queryConfig?: QueryConfig<typeof getInviteQueryOptions>;
}

export const useInvite = ({ inviteId, queryConfig }: UseInviteOptions) => {
  return useQuery({
    ...getInviteQueryOptions(inviteId),
    ...queryConfig,
  });
};
