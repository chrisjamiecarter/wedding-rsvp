import { queryOptions, useQuery } from "@tanstack/react-query";

import { api } from "@/lib/api-client";
import { QueryConfig } from "@/lib/react-query";
import { Food } from "@/types/api";

export const getFood = ({ foodId }: { foodId: string }): Promise<Food> => {
  return api.get(`/api/foodoptions/${foodId}`);
};

export const getFoodQueryOptions = (foodId: string) => {
  return queryOptions({
    queryKey: ["foods", foodId],
    queryFn: () => getFood({ foodId }),
  });
};

interface UseFoodOptions {
  foodId: string;
  queryConfig?: QueryConfig<typeof getFoodQueryOptions>;
}

export const useFood = ({ foodId, queryConfig }: UseFoodOptions) => {
  return useQuery({
    ...getFoodQueryOptions(foodId),
    ...queryConfig,
  });
};
