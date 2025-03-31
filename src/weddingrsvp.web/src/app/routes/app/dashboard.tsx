import { useUser } from "@/lib/auth";

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
