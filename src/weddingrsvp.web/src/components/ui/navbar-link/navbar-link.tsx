import { Link } from "react-router";
import { LucideProps } from "lucide-react";

import classes from "./navbar-link.module.css";

export interface NavbarLinkProps {
  link: string;
  label: string;
  icon: React.ForwardRefExoticComponent<
    Omit<LucideProps, "ref"> & React.RefAttributes<SVGSVGElement>
  >;
}

export const NavbarLink = (props: NavbarLinkProps) => {
  return (
    <Link className={classes.link} to={props.link}>
      <props.icon className={classes.linkIcon} strokeWidth={1.5} />
      <span>{props.label}</span>
    </Link>
  );
};
