import { Icon, IconLogout, IconProps } from "@tabler/icons-react";
import classes from "./navbar.module.css";
import { Button } from "@mantine/core";

interface NavbarData {
  link: string;
  label: string;
  icon: React.ForwardRefExoticComponent<IconProps & React.RefAttributes<Icon>>;
}

interface NavbarProps {
  navbarData: NavbarData[];
  signoutFn: () => Promise<void>;
}

export function Navbar(props: NavbarProps) {
  const links = props.navbarData.map((item) => (
    <a className={classes.link} href={item.link} key={item.label}>
      <item.icon className={classes.linkIcon} stroke={1.5} />
      <span>{item.label}</span>
    </a>
  ));

  return (
    <>
      <div className={classes.navbarMain}>{links}</div>

      <div className={classes.footer}>
        <Button
          className={classes.button}
          leftSection={
            <IconLogout className={classes.buttonLinkIcon} stroke={1.5} />
          }
          fullWidth
          justify="left"
          onClick={props.signoutFn}>
          Sign Out
        </Button>
      </div>
    </>
  );
}
