import { paths } from "@/configs/paths";
import Axios, { InternalAxiosRequestConfig } from "axios";

function authRequestInterceptor(config: InternalAxiosRequestConfig) {
  if (config.headers) {
    config.headers.Accept = "application/json";
  }

  config.withCredentials = true;
  return config;
}

export const api = Axios.create({
  //baseURL: "https://localhost:7156/api",
  baseURL: "/api", // This works with Vite proxy.
});

api.interceptors.request.use(authRequestInterceptor);
api.interceptors.response.use(
  (response) => {
    return response.data; // Unwrapping the data.
  },
  (error) => {
    const message = error.response?.data?.message || error.message;
    console.error("Error", message);

    if (error.response?.status === 401) {
      const searchParams = new URLSearchParams(window.location.search);
      const redirectTo =
        searchParams.get("redirectTo") || window.location.pathname;
      window.location.href = paths.auth.signin.getHref(redirectTo);
    }

    return Promise.reject(error);
  }
);
