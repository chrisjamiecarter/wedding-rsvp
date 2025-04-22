import { useMutation, useQueryClient } from "@tanstack/react-query";
import { z } from "zod";

import { api } from "@/lib/api-client";
import { MutationConfig } from "@/lib/react-query";

import { getInviteQueryOptions } from "@/features/invites/api/get-invite";
import { getGuestsQueryOptions } from "@/features/guests/api/get-guests";
import { RsvpStatuses } from "@/types/api";

export const submitRsvpInputSchema = z.object({
  inviteId: z.string().min(1, "Required"),
  token: z.string().min(1, "Required"),
  guests: z
    .array(
      z
        .object({
          guestId: z.string().min(1, "Required"),
          rsvpStatus: z.enum(RsvpStatuses, {
            errorMap: () => ({ message: "Please confirm if attending or not" }),
          }),
          mainFoodOptionId: z.string().nullable().optional(),
          dessertFoodOptionId: z.string().nullable().optional(),
        })
        .superRefine((guest, ctx) => {
          if (guest.rsvpStatus === "Attending") {
            if (!guest.mainFoodOptionId) {
              ctx.addIssue({
                code: z.ZodIssueCode.custom,
                message: "Please select a main course",
                path: ["mainFoodOptionId"],
              });
            }
            if (!guest.dessertFoodOptionId) {
              ctx.addIssue({
                code: z.ZodIssueCode.custom,
                message: "Please select a dessert",
                path: ["dessertFoodOptionId"],
              });
            }
          } else if (guest.rsvpStatus === "NotAttending") {
            if (guest.mainFoodOptionId) {
              ctx.addIssue({
                code: z.ZodIssueCode.custom,
                message:
                  "Main food selection should be empty when not attending",
                path: ["mainFoodOptionId"],
              });
            }
            if (guest.dessertFoodOptionId) {
              ctx.addIssue({
                code: z.ZodIssueCode.custom,
                message:
                  "Dessert food selection should be empty when not attending",
                path: ["dessertFoodOptionId"],
              });
            }
          }
        })
    )
    .nonempty("Can't be empty!"),
});

export type SubmitRsvpInput = z.infer<typeof submitRsvpInputSchema>;

export const submitRsvp = ({ data }: { data: SubmitRsvpInput }) => {
  console.log("RsvpData", data);
  return api.post(`/api/rsvps/${data.inviteId}`, data);
};

type UseSubmitRsvpOptions = {
  inviteId: string;
  mutationConfig?: MutationConfig<typeof submitRsvp>;
};

export const useSubmitRsvp = ({
  inviteId,
  mutationConfig,
}: UseSubmitRsvpOptions) => {
  const queryClient = useQueryClient();

  const { onSuccess, ...restConfig } = mutationConfig || {};

  return useMutation({
    onSuccess: (...args) => {
      queryClient.invalidateQueries({
        queryKey: getGuestsQueryOptions({ inviteId }).queryKey,
      });
      queryClient.invalidateQueries({
        queryKey: getInviteQueryOptions(inviteId).queryKey,
      });
      onSuccess?.(...args);
    },
    ...restConfig,
    mutationFn: submitRsvp,
  });
};
