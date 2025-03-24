import { z } from "zod";

import { api } from "./api-client";
import { createContext, useContext, useEffect, useState } from "react";
import { AxiosResponse } from "axios";

interface User {
  email: string;
}

interface AuthContextType {
  user: User | null;
  loading: boolean;
  signin: (email: string, password: string) => Promise<AxiosResponse<any, any>>;
  signout: () => Promise<AxiosResponse<any, any>>;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider = ({ children }: { children: ReactNode }) => {
  const [user, setUser] = useState<User | null>(null);
  const [loading, setLoading] = useState<boolean>(true);

  useEffect(() => {
    const fetchUser = async () => {
      try {
        const response = await manageInfo();
        console.log("fetchUser - response", response);
        const data: User = await response.data;
        console.log("fetchUser - data", data);
        setUser(data);
      } catch (error) {
        console.error("fetchUser - error", error);
      } finally {
        setLoading(false);
      }
    };
    fetchUser();
  }, []);

  const manageInfo = () => {
    const response = api.get("auth/manage/info");
    console.log("manageInfo - response", response);
    return response;
  };

  const signin = (email: string, password: string) => {
    const response = api.post("auth/login?useCookies=true", {
      email,
      password,
    });
    console.log("signin - response", response);
    return response;
  };

  const signout = () => {
    const response = api.post("auth/logout", {});
    console.log("signout - response", response);
    return response;
  };

  return (
    <AuthContext.Provider value={{ user, signin, signout, loading }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = (): AuthContextType => {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error("useAuth must be used within an AuthProvider");
  }
  return context;
};

// export const signinInputSchema = z.object({
//   email: z.string().min(1, "Required").email("Invalid email"),
//   password: z.string().min(1, "Required"),
// });
// export type SigninInput = z.infer<typeof signinInputSchema>;
