import { LoadingPage } from "@/components/ui/loading-page";
import { useRsvp } from "../api/get-rsvp";
import {
  Button,
  Card,
  Center,
  Group,
  NativeSelect,
  SimpleGrid,
  Text,
} from "@mantine/core";
import { useForm } from "@mantine/form";
import { useEffect } from "react";

const Rsvp = ({ inviteId, token }: { inviteId: string; token: string }) => {
  const rsvpQuery = useRsvp({
    inviteId,
    token,
  });

  const form = useForm({
    mode: "uncontrolled",
    initialValues: {
      inviteId: inviteId,
      token: token,
      guests: rsvpQuery.data?.guests.map((guest) => ({
        guestId: guest.guestId,
        name: guest.name,
        mainFoodOptionId: "",
        dessertFoodOptionId: "",
      })) || [
        {
          guestId: "",
          name: "",
          mainFoodOptionId: "",
          dessertFoodOptionId: "",
        },
      ],
    },
  });

  if (rsvpQuery.isLoading) {
    return <LoadingPage />;
  }

  const rsvp = rsvpQuery.data;

  if (!rsvp) return null;

  const mainOptions = [
    { label: "Select Main", value: "", disabled: true },
    ...rsvp.mains.map((main) => ({
      label: main.name,
      value: main.foodOptionId,
    })),
  ];

  const dessertOptions = [
    { label: "Select Dessert", value: "", disabled: true },
    ...rsvp.desserts.map((dessert) => ({
      label: dessert.name,
      value: dessert.foodOptionId,
    })),
  ];

  const fields = form.getValues().guests.map((item, index) => (
    <Card key={item.guestId} shadow="xs" p="lg" radius="md" withBorder>
      <Center>
        <Text fw="bold">{item.name}</Text>
      </Center>
      <NativeSelect
        mt="xs"
        label="Main"
        data={mainOptions}
        key={form.key(`guests.${index}.mainFoodOptionId`)}
        {...form.getInputProps(`guests.${index}.mainFoodOptionId`)}
      />
      <NativeSelect
        mt="xs"
        label="Dessert"
        data={dessertOptions}
        key={form.key(`guests.${index}.dessertFoodOptionId`)}
        {...form.getInputProps(`guests.${index}.dessertFoodOptionId`)}
      />
    </Card>
  ));

  return (
    <form
      id="create-rsvp"
      onSubmit={form.onSubmit((values) =>
        console.log("Form submitted: ", values)
      )}>
      <Group justify="end">
        <Button type="submit">Submit</Button>
      </Group>
      <SimpleGrid cols={4} mt="md">
        {fields}
      </SimpleGrid>
    </form>
  );
};

export default Rsvp;
