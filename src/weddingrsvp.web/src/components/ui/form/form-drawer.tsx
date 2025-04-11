import * as React from "react";
import { Button, Drawer } from "@mantine/core";
import { useDisclosure } from "@mantine/hooks";

export type FormDrawerProps = {
  isDone: boolean;
  submitButton: React.ReactElement;
  title: string;
  children: React.ReactNode;
};

export const FormDrawer = ({
  isDone,
  submitButton,
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
      <Button variant="default" onClick={open}>
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
        {submitButton}
      </Drawer>
    </>
  );
};
