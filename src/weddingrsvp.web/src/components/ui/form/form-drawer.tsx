import * as React from "react";
import { Button, Drawer, Group } from "@mantine/core";
import { useDisclosure } from "@mantine/hooks";

export type FormDrawerProps = {
  isDone: boolean;
  submitButton: React.ReactElement;
  triggerButtonIcon: React.ReactElement;
  title: string;
  children: React.ReactNode;
};

export const FormDrawer = ({
  isDone,
  submitButton,
  triggerButtonIcon,
  title,
  children,
}: FormDrawerProps) => {
  const [opened, { open, close }] = useDisclosure();

  React.useEffect(() => {
    if (isDone) {
      close();
    }
  }, [isDone, close]);

  return (
    <>
      <Button variant="default" onClick={open} leftSection={triggerButtonIcon}>
        {title}
      </Button>
      <Drawer
        offset={8}
        onClose={close}
        opened={opened}
        position="right"
        radius="md"
        size="xl"
        title={title}>
        {children}
        <Group justify="end" mt="lg">
          <Button variant="default" onClick={close}>
            Close
          </Button>
          {submitButton}
        </Group>
      </Drawer>
    </>
  );
};
