import { useMutation, useQueryClient } from "@tanstack/react-query";
import { z } from "zod";

import { api } from "@/lib/api-client";
import { MutationConfig } from "@/lib/react-query";
import { Guest } from "@/types/api";

import { getGuestQueryOptions } from "./get-guest";
import { getGuestsQueryOptions } from "./get-guests";

export const updateGuestInputSchema = z.object({
  inviteId: z.string().min(1, "Required"),
  name: z.string().min(1, "Required"),
  rsvpStatus: z.string().min(1, "Required"),
});

export type UpdateGuestInput = z.infer<typeof updateGuestInputSchema>;

export const updateGuest = ({
  data,
  guestId,
}: {
  data: UpdateGuestInput;
  guestId: string;
}): Promise<Guest> => {
  console.log("GuestData", data);
  return api.put(`/api/guests/${guestId}`, data);
};

type UseUpdateGuestOptions = {
  mutationConfig?: MutationConfig<typeof updateGuest>;
};

export const useUpdateGuest = ({
  mutationConfig,
}: UseUpdateGuestOptions = {}) => {
  const queryClient = useQueryClient();

  const { onSuccess, ...restConfig } = mutationConfig || {};

  return useMutation({
    onSuccess: (data, ...args) => {
      queryClient.invalidateQueries({
        queryKey: getGuestsQueryOptions({ eventId: data.inviteId }).queryKey,
      });
      queryClient.refetchQueries({
        queryKey: getGuestQueryOptions(data.id).queryKey,
      });
      onSuccess?.(data, ...args);
    },
    ...restConfig,
    mutationFn: updateGuest,
  });
};
