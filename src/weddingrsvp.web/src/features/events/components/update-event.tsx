import { Button, TextInput } from "@mantine/core";
import { DatePickerInput, TimeInput } from "@mantine/dates";
import { useForm, zodResolver } from "@mantine/form";

import { FormDrawer } from "@/components/ui/form/form-drawer";

import { updateEventInputSchema, useUpdateEvent } from "../api/update-event";
import { useEvent } from "../api/get-event";
import CustomNotifications from "@/components/ui/notifications/notifications";
import { Pencil } from "lucide-react";

type UpdateEventProps = {
  eventId: string;
};

export const UpdateEvent = ({ eventId }: UpdateEventProps) => {
  const eventQuery = useEvent({ eventId });
  const updateEventMutation = useUpdateEvent({
    mutationConfig: {
      onSuccess: () => {
        CustomNotifications.success({
          title: "Event Updated",
          message: "",
        });
      },
    },
  });

  const event = eventQuery.data;

  const form = useForm({
    mode: "uncontrolled",
    initialValues: {
      name: event?.name,
      description: event?.description,
      venue: event?.venue,
      address: event?.address,
      date: event?.date ? new Date(event.date) : new Date(),
      time: event?.time,
      dressCode: event?.dressCode,
    },
    validate: zodResolver(updateEventInputSchema),
  });

  return (
    <FormDrawer
      isDone={updateEventMutation.isSuccess}
      submitButton={
        <Button
          form="update-event"
          type="submit"
          loading={updateEventMutation.isPending}>
          Submit
        </Button>
      }
      triggerButtonIcon={<Pencil size={16} />}
      title="Update Event">
      <form
        id="update-event"
        onSubmit={form.onSubmit((values) => {
          const data = updateEventInputSchema.parse(values);
          updateEventMutation.mutate({ data: data, eventId });
        })}>
        <TextInput
          label="Name"
          placeholder="Example Event Name"
          key={form.key("name")}
          {...form.getInputProps("name")}
          required
          mt="md"
        />
        <TextInput
          label="Description"
          placeholder="Example Description"
          key={form.key("description")}
          {...form.getInputProps("description")}
          mt="md"
        />
        <TextInput
          label="Venue"
          placeholder="Example Venue Name"
          key={form.key("venue")}
          {...form.getInputProps("venue")}
          required
          mt="md"
        />
        <TextInput
          label="Address"
          placeholder="Example Address"
          key={form.key("address")}
          {...form.getInputProps("address")}
          required
          mt="md"
        />
        <DatePickerInput
          label="Date"
          placeholder="Pick date"
          key={form.key("date")}
          {...form.getInputProps("date")}
          required
          mt="md"
        />
        <TimeInput
          label="Time"
          placeholder="Pick time"
          withSeconds
          key={form.key("time")}
          {...form.getInputProps("time")}
          required
          mt="md"
        />
        <TextInput
          label="Dress Code"
          placeholder="Formal, Casual, Smart, etc."
          key={form.key("dressCode")}
          {...form.getInputProps("dressCode")}
          mt="md"
        />
      </form>
    </FormDrawer>
  );
};
