import { GoogleButton } from "@/components/ui/google-button";
import { signinWithEmailAndPassword } from "@/lib/auth";
import {
  Button,
  Checkbox,
  Divider,
  Paper,
  PasswordInput,
  TextInput,
} from "@mantine/core";
import { isEmail, useForm } from "@mantine/form";

const SigninForm = () => {
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

  const handleLogin = async (values: { email: string; password: string }) => {
    try {
      const response = await signinWithEmailAndPassword({
        email: values.email,
        password: values.password,
      });
      console.log("handleLogin response", response);
    } catch (error) {
      console.error("handleLogin error:", error);
    }
  };

  return (
    <Paper withBorder shadow="md" p={30} mt={20} radius="md">
      <GoogleButton fullWidth>Sign in with Google</GoogleButton>

      <Divider label="Or continue with email" labelPosition="center" my="lg" />
      <form onSubmit={form.onSubmit((values) => handleLogin(values))}>
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
        <Button type="submit" fullWidth mt="xl">
          Sign in
        </Button>
      </form>
    </Paper>
  );
};

export default SigninForm;
