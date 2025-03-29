import { useNavigate } from "react-router";
import { AppShell, Button, Image, NavLink, Text } from "@mantine/core";

import { paths } from "@/configs/paths";

import logo from "@/assets/logo.svg";
import { Link } from "@/components/ui/link";
import { useAuth } from "@/lib/auth";

const Logo = () => {
  return (
    <Link to={paths.home.getHref()}>
      <Image h="6rem" w="auto" radius="lg" src={logo} />
      <Text>Wedding RSVP</Text>
    </Link>
  );
};

const DashboardLayout = ({ children }: { children: React.ReactNode }) => {
  const navigate = useNavigate();

  const { signout } = useAuth();

  const handleSignout = async () => {
    await signout().finally(() =>
      navigate(paths.auth.signin.getHref(location.pathname))
    );
  };

  return (
    <AppShell
      header={{ height: 60 }}
      navbar={{ width: 300, breakpoint: "sm", collapsed: { mobile: !open } }}
      padding="md">
      <AppShell.Header>
        <Logo />
        <Button onClick={handleSignout}>Sign Out</Button>
      </AppShell.Header>
      <AppShell.Navbar p="md">
        Navbar
        <NavLink>Events</NavLink>
      </AppShell.Navbar>
      <AppShell.Main>{children}</AppShell.Main>
    </AppShell>
  );
};

export default DashboardLayout;
