import { paths } from "@/configs/paths";

const googleSignin = () => {
  const returnUrl = encodeURIComponent(
    `${window.location.origin}${paths.app.dashboard.getHref()}`
  );

  window.location.href = `https://localhost:7156/api/auth/google-login?returnUrl=${returnUrl}`;
};

export default googleSignin;
