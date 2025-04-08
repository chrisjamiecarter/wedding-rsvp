import { ContentLayout } from "@/components/layouts/content-layout";
import { LoadingPage } from "@/components/ui/loading-page";
import { useEvent } from "@/features/events/api/get-event";
import EventView from "@/features/events/components/event-view";
import { useParams } from "react-router";

const EventRoute = () => {
  const params = useParams();
  const eventId = params.eventId as string;
  const eventQuery = useEvent({ eventId });

  if (eventQuery.isLoading) {
    return <LoadingPage />;
  }

  const event = eventQuery.data;

  if (!event) return null;

  return (
    <ContentLayout title={event.name}>
      <EventView eventId={eventId} />
    </ContentLayout>
  );
};
export default EventRoute;
