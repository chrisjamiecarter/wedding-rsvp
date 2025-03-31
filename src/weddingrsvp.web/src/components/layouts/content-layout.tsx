import { Container, Stack, Title } from "@mantine/core";

type ContentLayoutProps = {
  children: React.ReactNode;
  title: string;
};

export const ContentLayout = ({ children, title }: ContentLayoutProps) => {
  return (
    <>
      <Container size="xl" px="0" py="lg" mx="auto">
        <Title order={2} fw={600}>
          {title}
        </Title>
        <Container size="xl" pb="lg" px="0" mx="auto">
          {children}
        </Container>
      </Container>
    </>
  );
};
