import { Button } from "@mantine/core";

import { ConfirmationDialog } from "@/components/ui/dialog/confirmation-dialog";

import { useDeleteInvite } from "../api/delete-invite";
import CustomNotifications from "@/components/ui/notifications/notifications";
import { Trash } from "lucide-react";

type DeleteInviteProps = {
  inviteId: string;
  eventId: string;
};

const DeleteInvite = ({ inviteId, eventId }: DeleteInviteProps) => {
  const deleteInviteMutation = useDeleteInvite({
    eventId,
    mutationConfig: {
      onSuccess: () => {
        CustomNotifications.success({
          title: "Invite Deleted",
          message: "",
        });
      },
    },
  });

  return (
    <ConfirmationDialog
      type="danger"
      title="Delete Invite"
      body="Are you sure you want to delete this invite?"
      isDone={deleteInviteMutation.isSuccess}
      confirmButton={
        <Button
          loading={deleteInviteMutation.isPending}
          type="button"
          color="red"
          onClick={() => deleteInviteMutation.mutate({ inviteId })}>
          Delete
        </Button>
      }
      triggerButtonContent={<Trash size={16} />}
    />
  );
};

export default DeleteInvite;
