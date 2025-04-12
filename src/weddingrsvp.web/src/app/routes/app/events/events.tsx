import { ContentLayout } from "@/components/layouts/content-layout";
import { CreateEvent } from "@/features/events/components/create-event";
import { EventsList } from "@/features/events/components/events-list";
import { Group } from "@mantine/core";
import { useQueryClient } from "@tanstack/react-query";

const EventsRoute = () => {
  const queryClient = useQueryClient();
  return (
    <ContentLayout title="Events">
      <Group justify="end">
        <CreateEvent />
      </Group>
      <EventsList
        onEventPrefetch={(id) => {
          // TODO: Prefetch the guests / foodoptions
          console.log("onEventPrefetch", id);
        }}
      />
    </ContentLayout>
  );
};
export default EventsRoute;
