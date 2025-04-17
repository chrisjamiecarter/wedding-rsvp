import {
  Button,
  Combobox,
  ComboboxOption,
  Input,
  InputBase,
  TextInput,
  useCombobox,
} from "@mantine/core";
import { useForm, zodResolver } from "@mantine/form";
import { Plus } from "lucide-react";

import { FormDrawer } from "@/components/ui/form/form-drawer";
import CustomNotifications from "@/components/ui/notifications/notifications";
import {
  createFoodInputSchema,
  useCreateFood,
} from "@/features/foods/api/create-food";

import { FoodTypes } from "@/types/api";

type CreateFoodProps = {
  eventId: string;
};

export const CreateFood = ({ eventId }: CreateFoodProps) => {
  const createFoodMutation = useCreateFood({
    eventId,
    mutationConfig: {
      onSuccess: () => {
        CustomNotifications.success({
          title: "Food Created",
          message: "",
        });
      },
    },
  });

  const form = useForm({
    mode: "uncontrolled",
    initialValues: {
      eventId: eventId,
      name: "",
      foodType: "",
    },
    validate: zodResolver(createFoodInputSchema),
  });

  const combobox = useCombobox({
    onDropdownClose: () => combobox.resetSelectedOption(),
  });

  return (
    <FormDrawer
      isDone={createFoodMutation.isSuccess}
      submitButton={
        <Button
          form="create-food"
          type="submit"
          loading={createFoodMutation.isPending}>
          Submit
        </Button>
      }
      triggerButtonIcon={<Plus size={16} />}
      title="Create Food">
      <form
        id="create-food"
        onSubmit={form.onSubmit((values) => {
          const data = createFoodInputSchema.parse(values);
          createFoodMutation.mutate({ data: data });
        })}>
        <TextInput
          label="Name"
          placeholder="Example Name"
          key={form.key("name")}
          {...form.getInputProps("name")}
          required
          mt="md"
        />
        <Combobox
          store={combobox}
          withinPortal={false}
          onOptionSubmit={(value) => {
            form.setFieldValue("foodType", value);
            combobox.closeDropdown();
          }}>
          <Combobox.Target>
            <InputBase
              component="button"
              type="button"
              pointer
              rightSection={<Combobox.Chevron />}
              onClick={() => combobox.toggleDropdown()}
              rightSectionPointerEvents="none"
              label="Food Type"
              key={form.key("foodType")}
              {...form.getInputProps("foodType")}
              required
              mt="md">
              {form.getValues().foodType || (
                <Input.Placeholder>Select Food Type</Input.Placeholder>
              )}
            </InputBase>
          </Combobox.Target>
          <Combobox.Dropdown>
            {FoodTypes.map((type) => (
              <ComboboxOption key={type} value={type}>
                {type}
              </ComboboxOption>
            ))}
          </Combobox.Dropdown>
        </Combobox>
      </form>
    </FormDrawer>
  );
};
