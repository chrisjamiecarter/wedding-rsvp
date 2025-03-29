import { Button, Container, Text, Title } from "@mantine/core";

export const MainErrorFallback = () => {
  return (
    <Container>
      <Title>Something is not right...</Title>
      <Text>Unfortunately something went wrong. Try refreshing the page.</Text>
      <Button onClick={() => window.location.assign(window.location.origin)}>
        Refresh
      </Button>
    </Container>
  );
};
