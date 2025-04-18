import { Button } from "@mantine/core";

import { ConfirmationDialog } from "@/components/ui/dialog/confirmation-dialog";

import { useDeleteGuest } from "../api/delete-guest";
import CustomNotifications from "@/components/ui/notifications/notifications";
import { Trash } from "lucide-react";

type DeleteGuestProps = {
  guestId: string;
  inviteId: string;
};

const DeleteGuest = ({ guestId, inviteId }: DeleteGuestProps) => {
  const deleteGuestMutation = useDeleteGuest({
    inviteId,
    mutationConfig: {
      onSuccess: () => {
        CustomNotifications.success({
          title: "Guest Deleted",
          message: "",
        });
      },
    },
  });

  return (
    <ConfirmationDialog
      type="danger"
      title="Delete Guest"
      body="Are you sure you want to delete this guest?"
      isDone={deleteGuestMutation.isSuccess}
      confirmButton={
        <Button
          loading={deleteGuestMutation.isPending}
          type="button"
          color="red"
          onClick={() => deleteGuestMutation.mutate({ guestId })}>
          Delete
        </Button>
      }
      triggerButtonContent={<Trash size={16} />}
    />
  );
};

export default DeleteGuest;
