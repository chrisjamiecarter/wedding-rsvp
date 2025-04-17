import { ContentLayout } from "@/components/layouts/content-layout";
import { LoadingPage } from "@/components/ui/loading-page";
import { useFood } from "@/features/foods/api/get-food";
import FoodView from "@/features/foods/components/food-view";
import { useParams } from "react-router";

const FoodRoute = () => {
  const params = useParams();
  const foodId = params.foodId as string;
  const foodQuery = useFood({ foodId });

  if (foodQuery.isLoading) {
    return <LoadingPage />;
  }

  const food = foodQuery.data;

  if (!food) return null;

  return (
    <ContentLayout title={food.name}>
      <FoodView foodId={foodId} />
    </ContentLayout>
  );
};
export default FoodRoute;
