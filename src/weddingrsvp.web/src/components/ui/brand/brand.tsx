import { paths } from "@/configs/paths";
import { Group, Image, Text } from "@mantine/core";
import { Link } from "react-router";
import logo from "@/assets/logo.svg";
import classes from "./brand.module.css";

export const Brand = () => {
  return (
    <Link className={classes.brandlink} to={paths.home.getHref()}>
      <Group gap="xs">
        <Image h="3rem" w="auto" radius="lg" src={logo} />
        <Text fw={700}>Wedding RSVP</Text>
      </Group>
    </Link>
  );
};
