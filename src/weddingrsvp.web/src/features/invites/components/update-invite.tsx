import { Button, TextInput, useCombobox } from "@mantine/core";
import { useForm, zodResolver } from "@mantine/form";

import { FormDrawer } from "@/components/ui/form/form-drawer";

import { updateInviteInputSchema, useUpdateInvite } from "../api/update-invite";
import { useInvite } from "../api/get-invite";
import CustomNotifications from "@/components/ui/notifications/notifications";
import { Pencil } from "lucide-react";

type UpdateInviteProps = {
  inviteId: string;
};

export const UpdateInvite = ({ inviteId }: UpdateInviteProps) => {
  const inviteQuery = useInvite({ inviteId });
  const updateInviteMutation = useUpdateInvite({
    mutationConfig: {
      onSuccess: () => {
        CustomNotifications.success({
          title: "Invite Updated",
          message: "",
        });
      },
    },
  });

  const invite = inviteQuery.data;

  const form = useForm({
    mode: "uncontrolled",
    initialValues: {
      eventId: invite?.eventId,
      email: invite?.email,
      householdName: invite?.householdName,
    },
    validate: zodResolver(updateInviteInputSchema),
  });

  const combobox = useCombobox({
    onDropdownClose: () => combobox.resetSelectedOption(),
  });

  return (
    <FormDrawer
      isDone={updateInviteMutation.isSuccess}
      submitButton={
        <Button
          form="update-invite"
          type="submit"
          loading={updateInviteMutation.isPending}>
          Submit
        </Button>
      }
      triggerButtonIcon={<Pencil size={16} />}
      title="Update Invite">
      <form
        id="update-invite"
        onSubmit={form.onSubmit((values) => {
          const data = updateInviteInputSchema.parse(values);
          updateInviteMutation.mutate({ data: data, inviteId });
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
