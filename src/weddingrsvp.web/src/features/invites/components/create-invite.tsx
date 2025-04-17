import { Button, TextInput } from "@mantine/core";
import { useForm, zodResolver } from "@mantine/form";
import { Plus } from "lucide-react";

import { FormDrawer } from "@/components/ui/form/form-drawer";
import CustomNotifications from "@/components/ui/notifications/notifications";
import {
  createInviteInputSchema,
  useCreateInvite,
} from "@/features/invites/api/create-invite";

type CreateInviteProps = {
  eventId: string;
};

export const CreateInvite = ({ eventId }: CreateInviteProps) => {
  const createInviteMutation = useCreateInvite({
    eventId,
    mutationConfig: {
      onSuccess: () => {
        CustomNotifications.success({
          title: "Invite Created",
          message: "",
        });
      },
    },
  });

  const form = useForm({
    mode: "uncontrolled",
    initialValues: {
      eventId: eventId,
      email: "",
      householdName: "",
    },
    validate: zodResolver(createInviteInputSchema),
  });

  return (
    <FormDrawer
      isDone={createInviteMutation.isSuccess}
      submitButton={
        <Button
          form="create-invite"
          type="submit"
          loading={createInviteMutation.isPending}>
          Submit
        </Button>
      }
      triggerButtonIcon={<Plus size={16} />}
      title="Create Invite">
      <form
        id="create-invite"
        onSubmit={form.onSubmit((values) => {
          const data = createInviteInputSchema.parse(values);
          createInviteMutation.mutate({ data: data });
        })}>
        <TextInput
          label="Household"
          placeholder="Example Household"
          key={form.key("householdName")}
          {...form.getInputProps("householdName")}
          required
          mt="md"
        />
        <TextInput
          label="Email"
          placeholder="email@example.com"
          key={form.key("email")}
          {...form.getInputProps("email")}
          required
          mt="md"
        />
      </form>
    </FormDrawer>
  );
};
