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
    </section>
  );
};

export default DashboardRoute;
