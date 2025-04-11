import { Button } from "@mantine/core";

import { ConfirmationDialog } from "@/components/ui/dialog/confirmation-dialog";

import { useDeleteEvent } from "../api/delete-event";

type DeleteEventProps = {
  id: string;
};

const DeleteEvent = ({ id }: DeleteEventProps) => {
  const deleteEventMutation = useDeleteEvent({
    mutationConfig: {
      onSuccess: () => {
        // TODO: NOTIFICATIONS.
        console.log("DeleteEvent - Success");
      },
    },
  });

  return (
    <ConfirmationDialog
      type="danger"
      title="Delete Event"
      body="Are you sure you want to delete this event?"
      confirmButton={
        <Button
          loading={deleteEventMutation.isPending}
          type="button"
          color="red"
          onClick={() => deleteEventMutation.mutate({ eventId: id })}>
          Delete
        </Button>
      }
    />
  );
};

export default DeleteEvent;
