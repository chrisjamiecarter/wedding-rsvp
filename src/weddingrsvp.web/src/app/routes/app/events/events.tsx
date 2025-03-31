import { ContentLayout } from "@/components/layouts/content-layout";
import { EventsList } from "@/features/events/components/events-list";
import { useQueryClient } from "@tanstack/react-query";

const EventsRoute = () => {
  const queryClient = useQueryClient();
  return (
    <ContentLayout title="Events">
      <EventsList />
    </ContentLayout>
  );
};
export default EventsRoute;
