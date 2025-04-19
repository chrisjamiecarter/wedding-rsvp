import { LoadingPage } from "@/components/ui/loading-page";
import { useRsvp } from "../api/get-rsvp";
import {
  Button,
  Card,
  Group,
  SimpleGrid,
  Text,
  TextInput,
} from "@mantine/core";
import { useForm } from "@mantine/form";
import { useEffect } from "react";

const Rsvp = ({ inviteId, token }: { inviteId: string; token: string }) => {
  const form = useForm({
    mode: "uncontrolled",
    initialValues: {
      inviteId: inviteId,
      token: token,
      guests: [
        {
          guestId: "",
          name: "",
          mainFoodOptionId: "",
          dessertFoodOptionId: "",
        },
      ],
    },
  });

  const rsvpQuery = useRsvp({
    inviteId,
    token,
  });

  useEffect(() => {
    if (rsvpQuery.data) {
      const mappedGuests = rsvpQuery.data.guests.map((guest) => ({
        guestId: guest.guestId,
        name: guest.name,
        mainFoodOptionId: "",
        dessertFoodOptionId: "",
      }));

      form.setValues({ inviteId, token, guests: mappedGuests });
    }
  }, [inviteId, token, rsvpQuery.data]);

  if (rsvpQuery.isLoading) {
    return <LoadingPage />;
  }

  const rsvp = rsvpQuery.data;

  if (!rsvp) return null;

  const fields = form.getValues().guests.map((item, index) => (
    <Card key={item.guestId} shadow="xs" p="lg" radius="md" withBorder>
      <Text fw="bold">{item.name}</Text>
      <TextInput
        label="Main"
        key={form.key(`guests.${index}.mainFoodOptionId`)}
        {...form.getInputProps(`guests.${index}.mainFoodOptionId`)}
        mt="md"
      />
      <TextInput
        label="Dessert"
        key={form.key(`guests.${index}.dessertFoodOptionId`)}
        {...form.getInputProps(`guests.${index}.dessertFoodOptionId`)}
        mt="md"
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
