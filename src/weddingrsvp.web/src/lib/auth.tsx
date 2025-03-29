import { User } from "@/types/api";
import { createContext, useContext, useEffect, useState } from "react";
import { api } from "./api-client";
import { Navigate, useLocation } from "react-router";
import { paths } from "@/configs/paths";

export interface SigninCredentials {
  email: string;
  password: string;
}

export interface SigninResult {
  success: boolean;
  message?: string;
}

interface AuthContextType {
  user: User | null;
  isAuthenticated: boolean;
  isLoading: boolean;
  checkAuthStatus: () => Promise<void>;
  signin: (credentials: SigninCredentials) => Promise<SigninResult>;
  signout: () => Promise<void>;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const useAuth = (): AuthContextType => {
  const context = useContext(AuthContext);
  if (context === undefined) {
    throw new Error("useAuth must be used within an AuthProvider");
  }
  return context;
};

interface AuthProviderProps {
  children: React.ReactNode;
}

export const AuthProvider = ({ children }: AuthProviderProps) => {
  const [user, setUser] = useState<User | null>(null);
  const [isLoading, setIsLoading] = useState<boolean>(true);

  console.log("AuthProvider started");

  useEffect(() => {
    checkAuthStatus();
    // console.log("Checking if user is logged in on mount");
    // // Check if user is logged in on mount.
    // api
    //   .get<User>("/auth/me")
    //   .then((response) => {
    //     console.log(response.data);
    //     setUser(response.data);
    //   })
    //   .catch(() => setUser(null))
    //   .finally(() => setIsLoading(false));
  }, []);

  const checkAuthStatus = async () => {
    console.log("checkAuthStatus started");
    try {
      setIsLoading(true);
      const response = await api.get("/api/auth/me");
      setUser(response);
    } catch (error) {
      console.error("checkAuthStatus failed: ", error);
      setUser(null);
    } finally {
      setIsLoading(false);
    }
  };

  const signin = async (
    credentials: SigninCredentials
  ): Promise<SigninResult> => {
    console.log("signin started");
    try {
      setIsLoading(true);
      await api.post("/api/auth/login?useSessionCookies=true", credentials);
      const response = await api.get("/api/auth/me");
      setUser(response);
      return { success: true };
    } catch (error) {
      console.error("signin failed: ", error);
      setUser(null);
      return { success: false, message: "Signin failed" };
    } finally {
      setIsLoading(false);
    }
  };

  const signout = async () => {
    console.log("signout started");
    try {
      await api.post("/api/auth/logout", {});
      setUser(null);
    } catch (error) {
      console.error("signout failed: ", error);
    }
  };

  const value: AuthContextType = {
    user,
    isAuthenticated: !!user,
    isLoading,
    checkAuthStatus,
    signin,
    signout,
  };

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
};

export const ProtectedRoute = ({ children }: { children: React.ReactNode }) => {
  console.log("ProtectedRoute Started");
  const { user, isLoading } = useAuth();
  const location = useLocation();

  if (isLoading) return <p>Loading...</p>;
  if (!user)
    return (
      <Navigate to={paths.auth.signin.getHref(location.pathname)} replace />
    );

  return children;
};
