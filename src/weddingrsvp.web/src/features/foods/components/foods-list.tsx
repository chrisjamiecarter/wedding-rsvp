import { useQueryClient } from "@tanstack/react-query";
import { useNavigate, useSearchParams } from "react-router";
import { useFoods } from "../api/get-foods";
import { LoadingPage } from "@/components/ui/loading-page";
import { Button, Group, Pagination, Table } from "@mantine/core";
import { Link } from "@/components/ui/link";
import { paths } from "@/configs/paths";
import { getFoodQueryOptions } from "../api/get-food";
import DeleteFood from "./delete-food";

export type FoodsListProps = {
  eventId: string;
};

export const FoodsList = ({ eventId }: FoodsListProps) => {
  const navigate = useNavigate();
  const [searchParams] = useSearchParams();

  const foodsQuery = useFoods({
    eventId: eventId,
    pageNumber: +(searchParams.get("foodsPage") || 1),
  });

  const queryClient = useQueryClient();

  if (foodsQuery.isLoading) {
    return <LoadingPage />;
  }

  const pageNumber = foodsQuery.data?.pageNumber ?? 1;
  const totalPages = foodsQuery.data?.totalPages ?? 1;
  const foods = foodsQuery.data?.items;

  if (!foods) return null;

  const rows = foods.map((food) => (
    <Table.Tr key={food.id}>
      <Table.Td>{food.name}</Table.Td>
      <Table.Td>{food.foodType}</Table.Td>
      <Table.Td>
        <Link
          onMouseEnter={() => {
            queryClient.prefetchQuery(getFoodQueryOptions(food.id));
          }}
          to={paths.app.food.getHref(food.id)}>
          <Button variant="transparent">View</Button>
        </Link>
      </Table.Td>
      <Table.Td>
        <DeleteFood foodId={food.id} eventId={food.eventId} />
      </Table.Td>
    </Table.Tr>
  ));

  function navigateToPage(value: number): void {
    searchParams.set("foodsPage", value.toString());
    const href = `${paths.app.event.getHref(
      eventId
    )}?${searchParams.toString()}`;
    navigate(href);
  }

  return (
    <>
      <Table highlightOnHover>
        <Table.Thead>
          <Table.Tr>
            <Table.Th>Name</Table.Th>
            <Table.Th>Food Type</Table.Th>
            <Table.Th></Table.Th>
            <Table.Th></Table.Th>
          </Table.Tr>
        </Table.Thead>
        <Table.Tbody>{rows}</Table.Tbody>
      </Table>
      <Group mt="lg" justify="end">
        <Pagination
          total={totalPages}
          value={pageNumber}
          onChange={navigateToPage}
          withEdges
        />
      </Group>
    </>
  );
};
