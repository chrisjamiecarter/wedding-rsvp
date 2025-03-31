import { LogOut } from "lucide-react";
import classes from "./navbar.module.css";
import { Button } from "@mantine/core";
import { NavbarLink, NavbarLinkProps } from "../navbar-link/navbar-link";

export interface NavbarProps {
  navbarData: NavbarLinkProps[];
  signoutFn: () => Promise<void>;
}

export function Navbar(props: NavbarProps) {
  const links = props.navbarData.map((item) => (
    <NavbarLink
      key={item.label}
      icon={item.icon}
      label={item.label}
      link={item.link}
    />
  ));

  return (
    <>
      <div className={classes.navbarMain}>{links}</div>

      <div className={classes.footer}>
        <Button
          className={classes.button}
          leftSection={
            <LogOut className={classes.buttonLinkIcon} strokeWidth={1.5} />
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
