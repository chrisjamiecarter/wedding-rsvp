import { useQueryClient } from "@tanstack/react-query";
import { useNavigate, useSearchParams } from "react-router";
import { useEvents } from "../api/get-events";
import { LoadingPage } from "@/components/ui/loading-page";
import { Button, Group, Pagination, Table } from "@mantine/core";
import { Link } from "@/components/ui/link";
import { paths } from "@/configs/paths";
import DeleteEvent from "./delete-event";
import { getEventQueryOptions } from "../api/get-event";

export type EventsListProps = {
  onEventPrefetch: (id: string) => void;
};

export const EventsList = ({ onEventPrefetch }: EventsListProps) => {
  const navigate = useNavigate();
  const [searchParams] = useSearchParams();

  const eventsQuery = useEvents({
    pageNumber: +(searchParams.get("page") || 1),
  });

  const queryClient = useQueryClient();

  if (eventsQuery.isLoading) {
    return <LoadingPage />;
  }

  const pageNumber = eventsQuery.data?.pageNumber ?? 1;
  const totalPages = eventsQuery.data?.totalPages ?? 1;
  const events = eventsQuery.data?.items;

  if (!events) return null;

  const rows = events.map((event) => (
    <Table.Tr key={event.id}>
      <Table.Td>{event.name}</Table.Td>
      <Table.Td>{event.description}</Table.Td>
      <Table.Td>{event.venue}</Table.Td>
      <Table.Td>{event.address}</Table.Td>
      <Table.Td>{`${event.date} ${event.time}`}</Table.Td>
      <Table.Td>{event.dressCode}</Table.Td>
      <Table.Td>
        <Link
          onMouseEnter={() => {
            queryClient.prefetchQuery(getEventQueryOptions(event.id));
            onEventPrefetch?.(event.id);
          }}
          to={paths.app.event.getHref(event.id)}>
          <Button variant="transparent">View</Button>
        </Link>
      </Table.Td>
      <Table.Td>
        <DeleteEvent id={event.id} />
      </Table.Td>
    </Table.Tr>
  ));

  function navigateToPage(value: number): void {
    const href = `${paths.app.events.getHref()}?page=${value}`;
    navigate(href);
  }

  return (
    <>
      <Table highlightOnHover>
        <Table.Thead>
          <Table.Tr>
            <Table.Th>Name</Table.Th>
            <Table.Th>Description</Table.Th>
            <Table.Th>Venue</Table.Th>
            <Table.Th>Address</Table.Th>
            <Table.Th>Date & Time</Table.Th>
            <Table.Th>Dress Code</Table.Th>
            <Table.Th></Table.Th>
            <Table.Th></Table.Th>
          </Table.Tr>
        </Table.Thead>
        <Table.Tbody>{rows}</Table.Tbody>
      </Table>
      <Group mt="lg" justify="end">
        <Pagination
          total={totalPages}
          value={pageNumber}
          onChange={navigateToPage}
          withEdges
        />
      </Group>
    </>
  );
};
