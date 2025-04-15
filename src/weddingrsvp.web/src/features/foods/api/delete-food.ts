import { useMutation, useQueryClient } from "@tanstack/react-query";

import { api } from "@/lib/api-client";
import { MutationConfig } from "@/lib/react-query";

import { getFoodsQueryOptions } from "./get-foods";

export const deleteFood = ({ foodId }: { foodId: string }) => {
  return api.delete(`/api/foodoptions/${foodId}`);
};

type UseDeleteFoodOptions = {
  eventId: string;
  mutationConfig?: MutationConfig<typeof deleteFood>;
};

export const useDeleteFood = ({
  eventId,
  mutationConfig,
}: UseDeleteFoodOptions) => {
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
    mutationFn: deleteFood,
  });
};
