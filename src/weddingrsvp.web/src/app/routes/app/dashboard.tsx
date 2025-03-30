import { useAuth } from "@/lib/auth";

const DashboardRoute = () => {
  const auth = useAuth();

  return (
    <section>
      <h1>
        Hello <b>{auth.user ? auth.user?.email : null}</b>
      </h1>
      <p>This page should only be accessible after login</p>
    </section>
  );
};

export default DashboardRoute;
