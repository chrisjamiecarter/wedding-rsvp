import { useQueryClient } from "@tanstack/react-query";
import { useSearchParams } from "react-router";
import { useEvents } from "../api/get-events";
import { LoadingPage } from "@/components/ui/loading-page";
import { Group, Text } from "@mantine/core";

export const EventsList = () => {
  const [searchParams] = useSearchParams();

  const eventsQuery = useEvents({
    page: +(searchParams.get("page") || 1),
  });

  const queryClient = useQueryClient();

  if (eventsQuery.isLoading) {
    return <LoadingPage />;
  }

  const events = eventsQuery.data?.items;

  if (!events) return null;

  const elements = events.map((item) => {
    return (
      <Group key={item.id}>
        <Text>ID = {item.id}</Text>
        <Text>Name = {item.name}</Text>
      </Group>
    );
  });

  return elements;
};
