import { GoogleButton } from "@/components/ui/google-button";
import {
  Button,
  Checkbox,
  Divider,
  Paper,
  PasswordInput,
  TextInput,
} from "@mantine/core";
import { isEmail, useForm } from "@mantine/form";
import { SigninCredentials, useAuth } from "@/lib/auth";

interface SigninFormProps {
  onSuccess: () => void;
}

// const signin = (email: string, password: string) => {
//   console.log("Starting signin");
//   const response = api.post("auth/login?useCookies=true", {
//     email,
//     password,
//   });
//   console.log("signin - response", response);
//   return response;
// };

const SigninForm = ({ onSuccess }: SigninFormProps) => {
  const { signin } = useAuth();

  const handleSignin = async (values: { email: string; password: string }) => {
    try {
      const credentials: SigninCredentials = {
        email: values.email,
        password: values.password,
      };
      await signin(credentials);
      onSuccess();
    } catch (error) {
      console.error(error);
    }
  };

  const form = useForm({
    mode: "uncontrolled",
    initialValues: {
      email: "",
      password: "",
    },
    validate: {
      email: isEmail("Invalid email"),
    },
  });

  return (
    <Paper withBorder shadow="md" p={30} mt={20} radius="md">
      <GoogleButton fullWidth>Sign in with Google</GoogleButton>

      <Divider label="Or continue with email" labelPosition="center" my="lg" />
      <form onSubmit={form.onSubmit((values) => handleSignin(values))}>
        <TextInput
          label="Email"
          placeholder="example@email.com"
          key={form.key("email")}
          {...form.getInputProps("email")}
          required
          mt="md"
        />
        <PasswordInput
          label="Password"
          placeholder="Your password"
          key={form.key("password")}
          {...form.getInputProps("password")}
          required
          mt="md"
        />
        <Checkbox label="Remember me" mt="lg" />
        {/* <Button type="submit" fullWidth mt="xl" loading={signin.isPending}> */}
        <Button type="submit" fullWidth mt="xl">
          Sign in
        </Button>
      </form>
    </Paper>
  );
};

export default SigninForm;
