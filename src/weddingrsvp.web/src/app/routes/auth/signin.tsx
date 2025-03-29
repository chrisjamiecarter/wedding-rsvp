import { useNavigate, useSearchParams } from "react-router";

import AuthLayout from "@/components/layouts/auth-layout";
import SigninForm from "@/features/auth/components/signin-form";
import { paths } from "@/configs/paths";

const SigninRoute = () => {
  const navigate = useNavigate();
  const [searchParams] = useSearchParams();
  const redirectTo = searchParams.get("redirectTo");

  return (
    <AuthLayout title="Please sign in">
      <SigninForm
        onSuccess={() => {
          navigate(
            `${redirectTo ? `${redirectTo}` : paths.app.dashboard.getHref()}`,
            {
              replace: true,
            }
          );
        }}
      />
    </AuthLayout>
  );
};

export default SigninRoute;
