import { useNavigate } from "react-router";
import { AppShell, Burger, Group, NavLink } from "@mantine/core";
import { useDisclosure } from "@mantine/hooks";
import { Calendar, House, Mail, Utensils, Ticket } from "lucide-react";

import { paths } from "@/configs/paths";

import { useLogout } from "@/lib/auth";
import { Brand } from "../ui/brand";
import { Navbar } from "../ui/navbar";

const navData = [
  { link: paths.app.dashboard.getHref(), label: "Dashboard", icon: House },
  {
    link: paths.app.events.getHref(),
    label: "Events",
    icon: Calendar,
  },
  { link: paths.app.invites.getHref(), label: "Invites", icon: Mail },
  { link: paths.app.guests.getHref(), label: "Guests", icon: Ticket },
  {
    link: paths.app.foods.getHref(),
    label: "Food Options",
    icon: Utensils,
  },
];

const DashboardLayout = ({ children }: { children: React.ReactNode }) => {
  const navigate = useNavigate();
  const [mobileOpened, { toggle: toggleMobile }] = useDisclosure();
  const [desktopOpened, { toggle: toggleDesktop }] = useDisclosure(true);

  const signout = useLogout({
    onSuccess: () => navigate(paths.auth.signin.getHref(location.pathname)),
  });

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
        <Navbar navbarData={navData} signoutFn={() => signout.mutate({})} />
      </AppShell.Navbar>
      <AppShell.Main>{children}</AppShell.Main>
    </AppShell>
  );
};

export default DashboardLayout;
