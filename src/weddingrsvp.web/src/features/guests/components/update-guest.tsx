import { Button, TextInput, useCombobox } from "@mantine/core";
import { useForm, zodResolver } from "@mantine/form";

import { FormDrawer } from "@/components/ui/form/form-drawer";

import { updateGuestInputSchema, useUpdateGuest } from "../api/update-guest";
import { useGuest } from "../api/get-guest";
import CustomNotifications from "@/components/ui/notifications/notifications";
import { Pencil } from "lucide-react";

type UpdateGuestProps = {
  guestId: string;
};

export const UpdateGuest = ({ guestId }: UpdateGuestProps) => {
  const guestQuery = useGuest({ guestId });
  const updateGuestMutation = useUpdateGuest({
    mutationConfig: {
      onSuccess: () => {
        CustomNotifications.success({
          title: "Guest Updated",
          message: "",
        });
      },
    },
  });

  const guest = guestQuery.data;

  const form = useForm({
    mode: "uncontrolled",
    initialValues: {
      inviteId: guest?.inviteId,
      name: guest?.name,
      rsvpStatus: guest?.rsvpStatus,
    },
    validate: zodResolver(updateGuestInputSchema),
  });

  const combobox = useCombobox({
    onDropdownClose: () => combobox.resetSelectedOption(),
  });

  return (
    <FormDrawer
      isDone={updateGuestMutation.isSuccess}
      submitButton={
        <Button
          form="update-guest"
          type="submit"
          loading={updateGuestMutation.isPending}>
          Submit
        </Button>
      }
      triggerButtonIcon={<Pencil size={16} />}
      title="Update Guest">
      <form
        id="update-guest"
        onSubmit={form.onSubmit((values) => {
          const data = updateGuestInputSchema.parse(values);
          updateGuestMutation.mutate({ data: data, guestId });
        })}>
        <TextInput
          label="Name"
          placeholder="Example Name"
          key={form.key("name")}
          {...form.getInputProps("name")}
          required
          mt="md"
        />
      </form>
    </FormDrawer>
  );
};
