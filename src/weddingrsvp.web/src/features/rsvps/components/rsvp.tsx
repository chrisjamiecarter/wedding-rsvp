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
import { useForm, zodResolver } from "@mantine/form";
import { RsvpStatusOptions } from "@/types/api";
import { useEffect, useRef } from "react";
import { submitRsvpInputSchema } from "../api/submit-rsvp";

const Rsvp = ({ inviteId, token }: { inviteId: string; token: string }) => {
  const rsvpQuery = useRsvp({
    inviteId,
    token,
  });

  // Use a ref to track initialization state
  const initialized = useRef(false);

  const form = useForm({
    mode: "uncontrolled",
    initialValues: {
      inviteId: inviteId,
      token: token,
      guests: [
        {
          guestId: "",
          name: "",
          rsvpStatus: "",
          mainFoodOptionId: "",
          dessertFoodOptionId: "",
        },
      ],
    },
    validate: zodResolver(submitRsvpInputSchema),
  });

  // Initialize form with API data once it's available
  useEffect(() => {
    if (rsvpQuery.data && !initialized.current) {
      const mappedGuests = rsvpQuery.data.guests.map((guest) => ({
        guestId: guest.guestId,
        name: guest.name,
        rsvpStatus: "",
        mainFoodOptionId: "",
        dessertFoodOptionId: "",
      }));

      form.setValues({ inviteId, token, guests: mappedGuests });
      initialized.current = true;
    }
  }, [rsvpQuery.data, inviteId, token, form]);

  const handleRsvpChange = (index: number, value: string) => {
    // Update the RSVP status.
    form.setFieldValue(`guests.${index}.rsvpStatus`, value);

    // If not attending, clear and disable food options.
    if (value === "NotAttending") {
      form.setFieldValue(`guests.${index}.mainFoodOptionId`, "");
      form.setFieldValue(`guests.${index}.dessertFoodOptionId`, "");
    }
  };

  if (rsvpQuery.isLoading) {
    return <LoadingPage />;
  }

  const rsvp = rsvpQuery.data;

  if (!rsvp) return null;

  const rsvpOptions = [
    { label: "Select RSVP", value: "", disabled: true },
    ...RsvpStatusOptions,
  ];

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

  const fields = form.getValues().guests.map((item, index) => {
    const isAttending = item.rsvpStatus === "Attending";
    const isFoodDisabled = !isAttending;

    return (
      <Card key={item.guestId} shadow="xs" p="lg" radius="md" withBorder>
        <Center>
          <Text fw="bold">{item.name}</Text>
        </Center>
        <NativeSelect
          mt="xs"
          label="RSVP"
          data={rsvpOptions}
          key={form.key(`guests.${index}.rsvpStatus`)}
          onChange={(event) =>
            handleRsvpChange(index, event.currentTarget.value)
          }
          value={item.rsvpStatus}
          error={form.errors[`guests.${index}.rsvpStatus`]}
          required
        />
        <NativeSelect
          mt="xs"
          label="Main"
          data={mainOptions}
          key={form.key(`guests.${index}.mainFoodOptionId`)}
          disabled={isFoodDisabled}
          required={!isFoodDisabled}
          {...form.getInputProps(`guests.${index}.mainFoodOptionId`)}
        />
        <NativeSelect
          mt="xs"
          label="Dessert"
          data={dessertOptions}
          key={form.key(`guests.${index}.dessertFoodOptionId`)}
          disabled={isFoodDisabled}
          required={!isFoodDisabled}
          {...form.getInputProps(`guests.${index}.dessertFoodOptionId`)}
        />
      </Card>
    );
  });

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
