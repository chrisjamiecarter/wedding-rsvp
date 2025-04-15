import { Group } from "@mantine/core";
import { CreateFood } from "./create-food";
import { FoodsList } from "./foods-list";

type FoodsProps = {
  eventId: string;
};

const Foods = ({ eventId }: FoodsProps) => {
  return (
    <>
      <Group justify="end" mt="sm">
        <CreateFood eventId={eventId} />
      </Group>
      <FoodsList eventId={eventId} />
    </>
  );
};

export default Foods;
