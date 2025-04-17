import { Button } from "@mantine/core";

import { ConfirmationDialog } from "@/components/ui/dialog/confirmation-dialog";

import { useDeleteFood } from "../api/delete-food";
import CustomNotifications from "@/components/ui/notifications/notifications";
import { Trash } from "lucide-react";

type DeleteFoodProps = {
  foodId: string;
  eventId: string;
};

const DeleteFood = ({ foodId, eventId }: DeleteFoodProps) => {
  const deleteFoodMutation = useDeleteFood({
    eventId,
    mutationConfig: {
      onSuccess: () => {
        CustomNotifications.success({
          title: "Food Deleted",
          message: "",
        });
      },
    },
  });

  return (
    <ConfirmationDialog
      type="danger"
      title="Delete Food"
      body="Are you sure you want to delete this food?"
      isDone={deleteFoodMutation.isSuccess}
      confirmButton={
        <Button
          loading={deleteFoodMutation.isPending}
          type="button"
          color="red"
          onClick={() => deleteFoodMutation.mutate({ foodId })}>
          Delete
        </Button>
      }
      triggerButtonContent={<Trash size={16} />}
    />
  );
};

export default DeleteFood;
