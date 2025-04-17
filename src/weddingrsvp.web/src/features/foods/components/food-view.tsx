import { Group, Table } from "@mantine/core";

import { LoadingPage } from "@/components/ui/loading-page";
import { useFood } from "@/features/foods/api/get-food";

import { UpdateFood } from "./update-food";

const FoodView = ({ foodId }: { foodId: string }) => {
  const foodQuery = useFood({
    foodId,
  });

  if (foodQuery.isLoading) {
    return <LoadingPage />;
  }

  const food = foodQuery.data;

  if (!food) return null;

  return (
    <>
      <Group justify="end">
        <UpdateFood foodId={foodId} />
      </Group>

      <Table variant="vertical">
        <Table.Tbody>
          <Table.Tr>
            <Table.Th>ID</Table.Th>
            <Table.Td>{food.id}</Table.Td>
          </Table.Tr>

          <Table.Tr>
            <Table.Th>Name</Table.Th>
            <Table.Td>{food.name}</Table.Td>
          </Table.Tr>

          <Table.Tr>
            <Table.Th>Food Type</Table.Th>
            <Table.Td>{food.foodType}</Table.Td>
          </Table.Tr>
        </Table.Tbody>
      </Table>
    </>
  );
};

export default FoodView;
