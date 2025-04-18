import { useMutation, useQueryClient } from "@tanstack/react-query";

import { api } from "@/lib/api-client";
import { MutationConfig } from "@/lib/react-query";

import { getGuestsQueryOptions } from "./get-guests";

export const deleteGuest = ({ guestId }: { guestId: string }) => {
  return api.delete(`/api/guests/${guestId}`);
};

type UseDeleteGuestOptions = {
  inviteId: string;
  mutationConfig?: MutationConfig<typeof deleteGuest>;
};

export const useDeleteGuest = ({
  inviteId,
  mutationConfig,
}: UseDeleteGuestOptions) => {
  const queryClient = useQueryClient();

  const { onSuccess, ...restConfig } = mutationConfig || {};

  return useMutation({
    onSuccess: (...args) => {
      queryClient.invalidateQueries({
        queryKey: getGuestsQueryOptions({ inviteId }).queryKey,
      });
      onSuccess?.(...args);
    },
    ...restConfig,
    mutationFn: deleteGuest,
  });
};
