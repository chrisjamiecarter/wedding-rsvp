import { notifications } from "@mantine/notifications";
import Icons from "./icons";

const baseConfig = {
  closeButtonProps: { "aria-label": "Hide notification" },
  withBorder: true,
};

export const CustomNotifications = {
  info: (props: { title: string; message: string }) =>
    notifications.show({
      ...baseConfig,
      icon: Icons.info,
      ...props,
    }),

  success: (props: { title: string; message: string }) =>
    notifications.show({
      ...baseConfig,
      icon: Icons.success,
      color: "green",
      ...props,
    }),

  warning: (props: { title: string; message: string }) =>
    notifications.show({
      ...baseConfig,
      icon: Icons.warning,
      color: "yellow",
      ...props,
    }),

  error: (props: { title: string; message: string }) =>
    notifications.show({
      ...baseConfig,
      icon: Icons.error,
      color: "red",
      ...props,
    }),
};

export default CustomNotifications;
