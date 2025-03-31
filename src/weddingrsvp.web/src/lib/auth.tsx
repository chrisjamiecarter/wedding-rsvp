import { configureAuth } from "react-query-auth";
import { Navigate, useLocation } from "react-router";
import { z } from "zod";

import { paths } from "@/configs/paths";
import { User } from "@/types/api";

import { api } from "./api-client";

// api call definitions for auth (types, schemas, requests):
// these are not part of features as this is a module shared across features

export const signinInputSchema = z.object({
  email: z.string().min(1, "Required").email("Invalid email"),
  password: z.string().min(6, "Required"),
  rememberMe: z.boolean().default(false),
});

export type SigninInput = z.infer<typeof signinInputSchema>;

const getUser = async (): Promise<User> => {
  const response = (await api.get("/api/auth/me")) as { user: User };
  return response.user;
};

const signin = (data: SigninInput): Promise<void> => {
  const url = `/api/auth/login?use${
    data.rememberMe ? "" : "Session"
  }Cookies=true`;
  return api.post(url, data);
};

const signout = (): Promise<void> => {
  return api.post("/api/auth/logout", {});
};

export const registerInputSchema = z.object({
  email: z.string().min(1, "Required"),
  password: z.string().min(6, "Required"),
});

export type RegisterInput = z.infer<typeof registerInputSchema>;

const register = (data: RegisterInput): Promise<void> => {
  return api.post("/api/auth/register", data);
};

const authConfig = {
  userFn: async () => {
    const response = await getUser();
    return response;
  },
  loginFn: async (data: SigninInput) => {
    await signin(data);
    const response = await getUser();
    return response;
  },
  registerFn: async (data: RegisterInput) => {
    await register(data);
    const response = await getUser();
    return response;
  },
  logoutFn: signout,
};

export const { useUser, useLogin, useLogout, useRegister, AuthLoader } =
  configureAuth(authConfig);

export const ProtectedRoute = ({ children }: { children: React.ReactNode }) => {
  const user = useUser();
  const location = useLocation();

  if (!user.data) {
    return (
      <Navigate to={paths.auth.signin.getHref(location.pathname)} replace />
    );
  }

  return children;
};
