import { useQueryClient } from "@tanstack/react-query";
import { useSearchParams } from "react-router";
import { useEvents } from "../api/get-events";
import { LoadingPage } from "@/components/ui/loading-page";
import { Table } from "@mantine/core";
import { Link } from "@/components/ui/link";
import { paths } from "@/configs/paths";

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

  const rows = events.map((event) => (
    <Table.Tr key={event.id}>
      <Table.Td>{event.name}</Table.Td>
      <Table.Td>{event.description}</Table.Td>
      <Table.Td>{event.venue}</Table.Td>
      <Table.Td>{event.address}</Table.Td>
      <Table.Td>{`${event.date} ${event.time}`}</Table.Td>
      <Table.Td>{event.dressCode}</Table.Td>
      <Table.Td>
        <Link to={paths.app.event.getHref(event.id)}>View</Link>
      </Table.Td>
    </Table.Tr>
  ));

  return (
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
        </Table.Tr>
      </Table.Thead>
      <Table.Tbody>{rows}</Table.Tbody>
    </Table>
  );
};
