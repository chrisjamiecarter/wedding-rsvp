import { useNavigate } from "react-router";
import { AppShell, Burger, Button, Group, NavLink } from "@mantine/core";
import { useDisclosure } from "@mantine/hooks";
import {
  IconCalendarEvent,
  IconHome,
  IconTicket,
  IconToolsKitchen,
  IconUser,
} from "@tabler/icons-react";

import { paths } from "@/configs/paths";

import { useAuth } from "@/lib/auth";
import { Brand } from "../ui/brand";
import { Navbar } from "../ui/navbar";

const navData = [
  { link: paths.app.dashboard.getHref(), label: "Dashboard", icon: IconHome },
  { link: "", label: "Events", icon: IconCalendarEvent },
  { link: "", label: "Invites", icon: IconTicket },
  { link: "", label: "Guests", icon: IconUser },
  { link: "", label: "Food Options", icon: IconToolsKitchen },
];

const DashboardLayout = ({ children }: { children: React.ReactNode }) => {
  const navigate = useNavigate();
  const [mobileOpened, { toggle: toggleMobile }] = useDisclosure();
  const [desktopOpened, { toggle: toggleDesktop }] = useDisclosure(true);

  const { signout } = useAuth();

  const handleSignout = async () => {
    await signout().finally(() =>
      navigate(paths.auth.signin.getHref(location.pathname))
    );
  };

  return (
    <AppShell
      header={{ height: 60 }}
      navbar={{
        width: 300,
        breakpoint: "sm",
        collapsed: { mobile: !mobileOpened, desktop: !desktopOpened },
      }}
      padding="md">
      <AppShell.Header>
        <Group h="100%" px="md" gap="xl">
          <Burger
            opened={mobileOpened}
            onClick={toggleMobile}
            hiddenFrom="sm"
            size="sm"
          />
          <Burger
            opened={desktopOpened}
            onClick={toggleDesktop}
            visibleFrom="sm"
            size="sm"
          />
          <NavLink component={Brand} />
        </Group>
      </AppShell.Header>
      <AppShell.Navbar p="md">
        <Navbar navbarData={navData} signoutFn={handleSignout} />
      </AppShell.Navbar>
      <AppShell.Main>{children}</AppShell.Main>
    </AppShell>
  );
};

export default DashboardLayout;
