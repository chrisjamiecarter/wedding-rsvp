import { Center } from "@mantine/core";

import { Loader } from "@/components/ui/loader";

export const LoadingPage = () => {
  return (
    <Center h="100vh">
      <Loader size="xl" />
    </Center>
  );
};
