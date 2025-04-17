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

import { FormDrawer } from "@/components/ui/form/form-drawer";

import { updateFoodInputSchema, useUpdateFood } from "../api/update-food";
import { useFood } from "../api/get-food";
import CustomNotifications from "@/components/ui/notifications/notifications";
import { Pencil } from "lucide-react";
import { FoodTypes } from "@/types/api";

type UpdateFoodProps = {
  foodId: string;
};

export const UpdateFood = ({ foodId }: UpdateFoodProps) => {
  const foodQuery = useFood({ foodId });
  const updateFoodMutation = useUpdateFood({
    mutationConfig: {
      onSuccess: () => {
        CustomNotifications.success({
          title: "Food Updated",
          message: "",
        });
      },
    },
  });

  const food = foodQuery.data;

  const form = useForm({
    mode: "uncontrolled",
    initialValues: {
      eventId: food?.eventId,
      name: food?.name,
      foodType: food?.foodType,
    },
    validate: zodResolver(updateFoodInputSchema),
  });

  const combobox = useCombobox({
    onDropdownClose: () => combobox.resetSelectedOption(),
  });

  return (
    <FormDrawer
      isDone={updateFoodMutation.isSuccess}
      submitButton={
        <Button
          form="update-food"
          type="submit"
          loading={updateFoodMutation.isPending}>
          Submit
        </Button>
      }
      triggerButtonIcon={<Pencil size={16} />}
      title="Update Food">
      <form
        id="update-food"
        onSubmit={form.onSubmit((values) => {
          const data = updateFoodInputSchema.parse(values);
          updateFoodMutation.mutate({ data: data, foodId });
        })}>
        <TextInput
          label="Name"
          placeholder="Example Food Name"
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
