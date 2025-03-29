import { useNavigate } from "react-router";
import {
  AppShell,
  Burger,
  Button,
  Group,
  Image,
  NavLink,
  Text,
} from "@mantine/core";
import { useDisclosure } from "@mantine/hooks";
import {
  IconAdjustments,
  IconCalendarStats,
  IconFileAnalytics,
  IconGauge,
  IconLock,
  IconNotes,
  IconPresentationAnalytics,
} from "@tabler/icons-react";

import { paths } from "@/configs/paths";

import logo from "@/assets/logo.svg";
import { Link } from "@/components/ui/link";
import { useAuth } from "@/lib/auth";

const Logo = () => {
  return (
    <Link to={paths.home.getHref()}>
      <Group>
        <Image h="3rem" w="auto" radius="lg" src={logo} />
        <Text>Wedding RSVP</Text>
      </Group>
    </Link>
  );
};

const navData = [
  { link: "", label: "Events", icon: IconGauge },
  { link: "", label: "Invites", icon: IconGauge },
  { link: "", label: "Guests", icon: IconGauge },
  { link: "", label: "Food Options", icon: IconGauge },
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

  const navLinks = navData.map((item) => (
    <a href={item.link} key={item.label} onClick={() => navigate(item.link)}>
      <item.icon />
      {item.label}
    </a>
  ));

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
        <Group h="100%" px="md" justify="space-between">
          <Group>
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
            <Logo />
          </Group>
          <Button onClick={handleSignout}>Sign Out</Button>
        </Group>
      </AppShell.Header>
      <AppShell.Navbar p="md">{navLinks}</AppShell.Navbar>
      <AppShell.Main>{children}</AppShell.Main>
    </AppShell>
  );
};

export default DashboardLayout;
