import { CircleAlert, CircleCheck, CircleX, Info } from "lucide-react";

const Icons = {
  info: <Info className="size-6 " aria-hidden="true" />,
  success: <CircleCheck className="size-6 " aria-hidden="true" />,
  warning: <CircleAlert className="size-6 " aria-hidden="true" />,
  error: <CircleX className="size-6 " aria-hidden="true" />,
};

export default Icons;
