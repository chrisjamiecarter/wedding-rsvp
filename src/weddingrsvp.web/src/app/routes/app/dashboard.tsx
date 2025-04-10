import CustomNotifications from "@/components/ui/notifications/notifications";
import { useUser } from "@/lib/auth";
import { Button } from "@mantine/core";
import { notifications } from "@mantine/notifications";

const DashboardRoute = () => {
  const user = useUser();

  return (
    <section>
      <h1>
        Hello <b>{user.data ? user.data.email : null}</b>
      </h1>
      <p>This page should only be accessible after login</p>
      <Button
        onClick={() =>
          CustomNotifications.info({
            title: "Heads up!",
            message: "This is an info notification",
          })
        }>
        Show Info Notification
      </Button>
      <Button
        onClick={() =>
          CustomNotifications.success({
            title: "Well done!",
            message: "This is a success notification",
          })
        }>
        Show Success Notification
      </Button>
      <Button
        onClick={() =>
          CustomNotifications.warning({
            title: "Watch out!",
            message: "This is a warning notification",
          })
        }>
        Show Warning Notification
      </Button>
      <Button
        onClick={() =>
          CustomNotifications.error({
            title: "Uh oh!",
            message: "This is an error notification",
          })
        }>
        Show Error Notification
      </Button>
    </section>
  );
};

export default DashboardRoute;
