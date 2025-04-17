import { AppShell, Center, Container, Image, Title } from "@mantine/core";

import logo from "@/assets/logo.svg";
import { Link } from "@/components/ui/link";
import { paths } from "@/configs/paths";

type LandingLayoutProps = {
  children: React.ReactNode;
  title: string;
};

const LandingLayout = ({ children, title }: LandingLayoutProps) => {
  return (
    <AppShell padding="md">
      <AppShell.Main>
        <Container size={420} my={40}>
          <Center>
            <Link to={paths.home.getHref()}>
              <Image h="6rem" w="auto" radius="lg" src={logo} />
            </Link>
          </Center>
          <Title ta="center">{title}</Title>
          {children}
        </Container>
      </AppShell.Main>
    </AppShell>
  );
};

export default LandingLayout;
