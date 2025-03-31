import { useNavigate } from "react-router";

import { paths } from "@/configs/paths";
import { Button } from "@mantine/core";
import { useUser } from "@/lib/auth";

const LandingRoute = () => {
  const navigate = useNavigate();
  const user = useUser();

  const handleStart = () => {
    if (user.data) {
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
