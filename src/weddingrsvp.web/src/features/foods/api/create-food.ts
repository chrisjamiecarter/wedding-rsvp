import { useMutation, useQueryClient } from "@tanstack/react-query";
import { z } from "zod";

import { api } from "@/lib/api-client";
import { MutationConfig } from "@/lib/react-query";
import { Food, FoodTypes } from "@/types/api";

import { getFoodsQueryOptions } from "./get-foods";

export const createFoodInputSchema = z.object({
  eventId: z.string().min(1, "Required"),
  name: z.string().min(1, "Required"),
  foodType: z.enum(FoodTypes),
});

export type CreateFoodInput = z.infer<typeof createFoodInputSchema>;

export const createFood = ({
  data,
}: {
  data: CreateFoodInput;
}): Promise<Food> => {
  console.log("FoodData", data);
  return api.post(`/api/events/${data.eventId}/foodoptions`, data);
};

type UseCreateFoodOptions = {
  eventId: string;
  mutationConfig?: MutationConfig<typeof createFood>;
};

export const useCreateFood = ({
  eventId,
  mutationConfig,
}: UseCreateFoodOptions) => {
  const queryClient = useQueryClient();

  const { onSuccess, ...restConfig } = mutationConfig || {};

  return useMutation({
    onSuccess: (...args) => {
      queryClient.invalidateQueries({
        queryKey: getFoodsQueryOptions({ eventId }).queryKey,
      });
      onSuccess?.(...args);
    },
    ...restConfig,
    mutationFn: createFood,
  });
};
