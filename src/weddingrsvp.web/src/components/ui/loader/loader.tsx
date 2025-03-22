import { Loader as MantineLoader } from "@mantine/core";

const colors = {
  gray: "gray",
};

const sizes = {
  xs: "xs",
  sm: "sm",
  md: "md",
  lg: "lg",
  xl: "xl",
};

const types = {
  oval: "oval",
  bars: "bars",
  dots: "dots",
};

export type LoaderProps = {
  color?: keyof typeof colors;
  size?: keyof typeof sizes;
  type?: keyof typeof types;
};

export const Loader = ({
  color = "gray",
  size = "md",
  type = "oval",
}: LoaderProps) => {
  return <MantineLoader color={color} size={size} type={type} />;
};
