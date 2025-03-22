import Axios, { InternalAxiosRequestConfig } from "axios";

function authRequestInterceptor(config: InternalAxiosRequestConfig) {
  config.withCredentials = true;
  return config;
}

export const api = Axios.create({
  baseURL: "https://localhost:7156/api",
});

api.interceptors.request.use(authRequestInterceptor);
