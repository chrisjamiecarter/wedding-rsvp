import { AppShell, Container } from "@mantine/core";

type RsvpLayoutProps = {
  children: React.ReactNode;
};

const RsvpLayout = ({ children }: RsvpLayoutProps) => {
  return (
    <AppShell padding="md">
      <AppShell.Main>
        <Container size="xl" my={40}>
          {children}
        </Container>
      </AppShell.Main>
    </AppShell>
  );
};

export default RsvpLayout;
