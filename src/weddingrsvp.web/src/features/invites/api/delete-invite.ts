import { useMutation, useQueryClient } from "@tanstack/react-query";

import { api } from "@/lib/api-client";
import { MutationConfig } from "@/lib/react-query";

import { getInvitesQueryOptions } from "./get-invites";

export const deleteInvite = ({ inviteId }: { inviteId: string }) => {
  return api.delete(`/api/invites/${inviteId}`);
};

type UseDeleteInviteOptions = {
  eventId: string;
  mutationConfig?: MutationConfig<typeof deleteInvite>;
};

export const useDeleteInvite = ({
  eventId,
  mutationConfig,
}: UseDeleteInviteOptions) => {
  const queryClient = useQueryClient();

  const { onSuccess, ...restConfig } = mutationConfig || {};

  return useMutation({
    onSuccess: (...args) => {
      queryClient.invalidateQueries({
        queryKey: getInvitesQueryOptions({ eventId }).queryKey,
      });
      onSuccess?.(...args);
    },
    ...restConfig,
    mutationFn: deleteInvite,
  });
};
