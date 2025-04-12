import { FormDrawer } from "@/components/ui/form/form-drawer";
import CustomNotifications from "@/components/ui/notifications/notifications";
import {
  createEventInputSchema,
  useCreateEvent,
} from "@/features/events/api/create-event";
import { Button, TextInput } from "@mantine/core";
import { DatePickerInput, TimeInput } from "@mantine/dates";
import { useForm, zodResolver } from "@mantine/form";

export const CreateEvent = () => {
  const createEventMutation = useCreateEvent({
    mutationConfig: {
      onSuccess: () => {
        CustomNotifications.success({
          title: "Event Created",
          message: "",
        });
      },
    },
  });

  const form = useForm({
    mode: "uncontrolled",
    initialValues: {
      name: "",
      description: "",
      venue: "",
      address: "",
      date: new Date(),
      time: "00:00:00",
      dressCode: "",
    },
    validate: zodResolver(createEventInputSchema),
  });

  return (
    <FormDrawer
      isDone={createEventMutation.isSuccess}
      submitButton={
        <Button
          form="create-event"
          type="submit"
          loading={createEventMutation.isPending}>
          Submit
        </Button>
      }
      title="Create Event">
      <form
        id="create-event"
        onSubmit={form.onSubmit((values) => {
          const data = createEventInputSchema.parse(values);
          createEventMutation.mutate({ data: data });
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
