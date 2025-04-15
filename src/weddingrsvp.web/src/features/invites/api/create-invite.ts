import { useMutation, useQueryClient } from "@tanstack/react-query";
import { z } from "zod";

import { api } from "@/lib/api-client";
import { MutationConfig } from "@/lib/react-query";
import { Invite } from "@/types/api";

import { getInvitesQueryOptions } from "./get-invites";

export const createInviteInputSchema = z.object({
  eventId: z.string().min(1, "Required"),
  email: z.string().min(1, "Required").email("Invalid email"),
  householdName: z.string().min(1, "Required"),
});

export type CreateInviteInput = z.infer<typeof createInviteInputSchema>;

export const createInvite = ({
  data,
}: {
  data: CreateInviteInput;
}): Promise<Invite> => {
  console.log("InviteData", data);
  return api.post(`/api/events/${data.eventId}/invites`, data);
};

type UseCreateInviteOptions = {
  eventId: string;
  mutationConfig?: MutationConfig<typeof createInvite>;
};

export const useCreateInvite = ({
  eventId,
  mutationConfig,
}: UseCreateInviteOptions) => {
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
    mutationFn: createInvite,
  });
};
