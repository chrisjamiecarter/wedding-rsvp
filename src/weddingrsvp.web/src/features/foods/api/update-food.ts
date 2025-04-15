import { useMutation, useQueryClient } from "@tanstack/react-query";
import { z } from "zod";

import { api } from "@/lib/api-client";
import { MutationConfig } from "@/lib/react-query";
import { Food, FoodTypes } from "@/types/api";

import { getFoodQueryOptions } from "./get-food";

export const updateFoodInputSchema = z.object({
  eventId: z.string().min(1, "Required"),
  name: z.string().min(1, "Required"),
  foodType: z.enum(FoodTypes),
});

export type UpdateFoodInput = z.infer<typeof updateFoodInputSchema>;

export const updateFood = ({
  data,
  foodId,
}: {
  data: UpdateFoodInput;
  foodId: string;
}): Promise<Food> => {
  console.log("FoodData", data);
  return api.put(`/api/foodoptions/${foodId}`, data);
};

type UseUpdateFoodOptions = {
  mutationConfig?: MutationConfig<typeof updateFood>;
};

export const useUpdateFood = ({
  mutationConfig,
}: UseUpdateFoodOptions = {}) => {
  const queryClient = useQueryClient();

  const { onSuccess, ...restConfig } = mutationConfig || {};

  return useMutation({
    onSuccess: (data, ...args) => {
      queryClient.refetchQueries({
        queryKey: getFoodQueryOptions(data.id).queryKey,
      });
      onSuccess?.(data, ...args);
    },
    ...restConfig,
    mutationFn: updateFood,
  });
};
