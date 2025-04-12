import * as React from "react";
import { useDisclosure } from "@mantine/hooks";
import { Button, Group, Modal } from "@mantine/core";
import { CircleAlert, Info } from "lucide-react";

export type ConfirmationDialogProps = {
  confirmButton: React.ReactElement;
  triggerButtonContent: React.ReactElement;
  title: string;
  action?: string;
  body?: string;
  cancelButtonText?: string;
  type?: "danger" | "info";
  isDone?: boolean;
};

export const ConfirmationDialog = ({
  confirmButton,
  triggerButtonContent,
  title,
  body = "Are you sure you want to delete?",
  cancelButtonText = "Cancel",
  type = "danger",
  isDone = false,
}: ConfirmationDialogProps) => {
  const [opened, { open, close }] = useDisclosure();

  React.useEffect(() => {
    if (isDone) {
      close();
    }
  }, [isDone, close]);

  return (
    <>
      <Button color={type === "danger" ? "red" : ""} onClick={open}>
        {triggerButtonContent}
      </Button>
      <Modal.Root opened={opened} onClose={close} centered>
        <Modal.Overlay />
        <Modal.Content>
          <Modal.Header>
            <Modal.Title fw="bold">
              <Group>
                {type === "danger" && (
                  <CircleAlert
                    className="size-6"
                    color="red"
                    aria-hidden="true"
                  />
                )}
                {type === "info" && (
                  <Info className="size-6" aria-hidden="true" />
                )}
                {title}
              </Group>
            </Modal.Title>
            <Modal.CloseButton aria-label="Close dialog" />
          </Modal.Header>
          <Modal.Body>
            {body}
            <Group mt="lg" justify="flex-end">
              <Button variant="default" onClick={close}>
                {cancelButtonText}
              </Button>
              {confirmButton}
            </Group>
          </Modal.Body>
        </Modal.Content>
      </Modal.Root>
    </>
  );
};
