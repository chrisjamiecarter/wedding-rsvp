import { useNavigate } from "react-router";

import { paths } from "@/configs/paths";
import { Button } from "@mantine/core";
import { useAuth } from "@/lib/auth";
import { api } from "@/lib/api-client";

const test = async () => {
  console.log("starting test");
  const response = api.get("/auth/me");
  console.log("response", response);
};

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
      <Button onClick={test}>Test</Button>
    </>
  );
};

export default LandingRoute;
