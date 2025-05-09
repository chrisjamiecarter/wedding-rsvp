import { useForm } from "@mantine/form";
import { zodResolver } from "mantine-form-zod-resolver";
import { GoogleButton } from "@/components/ui/google-button";
import {
  Button,
  Checkbox,
  Divider,
  Paper,
  PasswordInput,
  TextInput,
} from "@mantine/core";
import { signinInputSchema, useLogin } from "@/lib/auth";
import googleSignin from "../api/google-signin";

interface SigninFormProps {
  onSuccess: () => void;
}

const SigninForm = ({ onSuccess }: SigninFormProps) => {
  const signin = useLogin({
    onSuccess,
  });

  const form = useForm({
    mode: "uncontrolled",
    initialValues: {
      email: "",
      password: "",
      rememberMe: false,
    },
    validate: zodResolver(signinInputSchema),
  });

  return (
    <Paper withBorder shadow="md" p={30} mt={20} radius="md">
      <GoogleButton fullWidth onClick={googleSignin}>
        Sign in with Google
      </GoogleButton>

      <Divider label="Or continue with email" labelPosition="center" my="lg" />
      <form onSubmit={form.onSubmit((values) => signin.mutate(values))}>
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
        <Checkbox
          label="Remember me"
          key={form.key("rememberMe")}
          {...form.getInputProps("rememberMe")}
          mt="lg"
        />
        <Button type="submit" fullWidth mt="xl" loading={signin.isPending}>
          Sign in
        </Button>
      </form>
    </Paper>
  );
};

export default SigninForm;
