import AuthLayout from "@/components/layouts/auth-layout";
import SigninForm from "@/features/auth/components/signin-form";

const SigninRoute = () => {
  return (
    <AuthLayout title="Please sign in">
      <SigninForm />
    </AuthLayout>
  );
};

export default SigninRoute;
