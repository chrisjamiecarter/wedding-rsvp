import { useMutation, useQueryClient } from "@tanstack/react-query";
import { z } from "zod";

import { api } from "@/lib/api-client";
import { MutationConfig } from "@/lib/react-query";
import { Guest } from "@/types/api";

import { getGuestsQueryOptions } from "./get-guests";

export const createGuestInputSchema = z.object({
  inviteId: z.string().min(1, "Required"),
  name: z.string().min(1, "Required"),
});

export type CreateGuestInput = z.infer<typeof createGuestInputSchema>;

export const createGuest = ({
  data,
}: {
  data: CreateGuestInput;
}): Promise<Guest> => {
  console.log("GuestData", data);
  return api.post(`/api/invites/${data.inviteId}/guests`, data);
};

type UseCreateGuestOptions = {
  inviteId: string;
  mutationConfig?: MutationConfig<typeof createGuest>;
};

export const useCreateGuest = ({
  inviteId,
  mutationConfig,
}: UseCreateGuestOptions) => {
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
    mutationFn: createGuest,
  });
};
