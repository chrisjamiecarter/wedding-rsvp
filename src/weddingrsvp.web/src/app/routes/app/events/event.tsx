import { LoaderFunctionArgs, useParams } from "react-router";
import { ErrorBoundary } from "react-error-boundary";

import { Mail, Utensils } from "lucide-react";
import { Space, Tabs } from "@mantine/core";

import { ContentLayout } from "@/components/layouts/content-layout";
import { LoadingPage } from "@/components/ui/loading-page";
import {
  getEventQueryOptions,
  useEvent,
} from "@/features/events/api/get-event";
import EventView from "@/features/events/components/event-view";
import Invites from "@/features/invites/components/invites";
import Foods from "@/features/foods/components/foods";
import { QueryClient } from "@tanstack/react-query";
import { getInvitesQueryOptions } from "@/features/invites/api/get-invites";

export const clientLoader =
  (queryClient: QueryClient) =>
  async ({ params }: LoaderFunctionArgs) => {
    const eventId = params.eventId as string;

    const eventQuery = getEventQueryOptions(eventId);
    const invitesQuery = getInvitesQueryOptions({ eventId });

    const promises = [
      queryClient.getQueryData(eventQuery.queryKey) ??
        (await queryClient.fetchQuery(eventQuery)),
      queryClient.getQueryData(invitesQuery.queryKey) ??
        (await queryClient.fetchQuery(invitesQuery)),
    ] as const;

    const [event, invites] = await Promise.all(promises);

    return {
      event,
      invites,
    };
  };

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
      <Space h="xl" />
      <Tabs defaultValue="invites">
        <Tabs.List grow>
          <Tabs.Tab value="invites" leftSection={<Mail />}>
            Invites
          </Tabs.Tab>
          <Tabs.Tab value="foods" leftSection={<Utensils />}>
            Food Options
          </Tabs.Tab>
        </Tabs.List>

        <Tabs.Panel value="invites">
          <ErrorBoundary
            fallback={
              <div>Failed to load invites. Try to refresh the page.</div>
            }>
            <Invites eventId={eventId} />
          </ErrorBoundary>
        </Tabs.Panel>
        <Tabs.Panel value="foods">
          <ErrorBoundary
            fallback={
              <div>Failed to load food options. Try to refresh the page.</div>
            }>
            <Foods eventId={eventId} />
          </ErrorBoundary>
        </Tabs.Panel>
      </Tabs>
    </ContentLayout>
  );
};
export default EventRoute;
