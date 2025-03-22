import { Loader } from "@/components/ui/loader/loader";

function LandingRoute() {
  return (
    <>
      <h1>Hello from LandingRoute</h1>;
      <Loader type="bars" />
      <Loader type="dots" />
      <Loader type="oval" />
    </>
  );
}

export default LandingRoute;
