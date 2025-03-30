import { useNavigate } from "react-router";

import { paths } from "@/configs/paths";
import { Button } from "@mantine/core";
import { useAuth } from "@/lib/auth";

const LandingRoute = () => {
  const navigate = useNavigate();
  const auth = useAuth();

  const handleStart = () => {
    if (auth.user) {
      navigate(paths.app.dashboard.getHref());
    } else {
      navigate(paths.auth.signin.getHref());
    }
  };
  return (
    <>
      <h1>Hello from LandingRoute</h1>;
      <Button onClick={handleStart}>Get started</Button>
    </>
  );
};

export default LandingRoute;
