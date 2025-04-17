import { useMutation, useQueryClient } from "@tanstack/react-query";
import { z } from "zod";

import { api } from "@/lib/api-client";
import { MutationConfig } from "@/lib/react-query";
import { Invite } from "@/types/api";

import { getInviteQueryOptions } from "./get-invite";
import { getInvitesQueryOptions } from "./get-invites";

export const updateInviteInputSchema = z.object({
  eventId: z.string().min(1, "Required"),
  email: z.string().min(1, "Required").email("Invalid email"),
  householdName: z.string().min(1, "Required"),
});

export type UpdateInviteInput = z.infer<typeof updateInviteInputSchema>;

export const updateInvite = ({
  data,
  inviteId,
}: {
  data: UpdateInviteInput;
  inviteId: string;
}): Promise<Invite> => {
  console.log("InviteData", data);
  return api.put(`/api/invites/${inviteId}`, data);
};

type UseUpdateInviteOptions = {
  mutationConfig?: MutationConfig<typeof updateInvite>;
};

export const useUpdateInvite = ({
  mutationConfig,
}: UseUpdateInviteOptions = {}) => {
  const queryClient = useQueryClient();

  const { onSuccess, ...restConfig } = mutationConfig || {};

  return useMutation({
    onSuccess: (data, ...args) => {
      queryClient.invalidateQueries({
        queryKey: getInvitesQueryOptions({ eventId: data.eventId }).queryKey,
      });
      queryClient.refetchQueries({
        queryKey: getInviteQueryOptions(data.id).queryKey,
      });
      onSuccess?.(data, ...args);
    },
    ...restConfig,
    mutationFn: updateInvite,
  });
};
