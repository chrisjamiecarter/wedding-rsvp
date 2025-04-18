import { Button, TextInput } from "@mantine/core";
import { useForm, zodResolver } from "@mantine/form";
import { Plus } from "lucide-react";

import { FormDrawer } from "@/components/ui/form/form-drawer";
import CustomNotifications from "@/components/ui/notifications/notifications";
import {
  createGuestInputSchema,
  useCreateGuest,
} from "@/features/guests/api/create-guest";

type CreateGuestProps = {
  inviteId: string;
};

export const CreateGuest = ({ inviteId }: CreateGuestProps) => {
  const createGuestMutation = useCreateGuest({
    inviteId,
    mutationConfig: {
      onSuccess: () => {
        CustomNotifications.success({
          title: "Guest Created",
          message: "",
        });
      },
    },
  });

  const form = useForm({
    mode: "uncontrolled",
    initialValues: {
      inviteId: inviteId,
      name: "",
    },
    validate: zodResolver(createGuestInputSchema),
  });

  return (
    <FormDrawer
      isDone={createGuestMutation.isSuccess}
      submitButton={
        <Button
          form="create-guest"
          type="submit"
          loading={createGuestMutation.isPending}>
          Submit
        </Button>
      }
      triggerButtonIcon={<Plus size={16} />}
      title="Create Guest">
      <form
        id="create-guest"
        onSubmit={form.onSubmit((values) => {
          const data = createGuestInputSchema.parse(values);
          createGuestMutation.mutate({ data: data });
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
