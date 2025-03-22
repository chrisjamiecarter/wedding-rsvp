import { z } from "zod";

import { api } from "./api-client";

export const signinInputSchema = z.object({
  email: z.string().min(1, "Required").email("Invalid email"),
  password: z.string().min(1, "Required"),
});
export type SigninInput = z.infer<typeof signinInputSchema>;

export const signinWithEmailAndPassword = (data: SigninInput) => {
  const response = api.post("auth/login?useCookies=true", data);
  return response;
};
