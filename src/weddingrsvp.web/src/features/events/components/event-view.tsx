import { format } from "date-fns";
import { LoadingPage } from "@/components/ui/loading-page";
import { useEvent } from "@/features/events/api/get-event";
import { Group, Table } from "@mantine/core";
import { UpdateEvent } from "./update-event";

const EventView = ({ eventId }: { eventId: string }) => {
  const eventQuery = useEvent({
    eventId,
  });

  if (eventQuery.isLoading) {
    return <LoadingPage />;
  }

  const event = eventQuery.data;

  if (!event) return null;

  return (
    <>
      <Group justify="end">
        <UpdateEvent eventId={eventId} />
      </Group>

      <Table variant="vertical">
        <Table.Tbody>
          <Table.Tr>
            <Table.Th>ID</Table.Th>
            <Table.Td>{event.id}</Table.Td>
          </Table.Tr>

          <Table.Tr>
            <Table.Th>Name</Table.Th>
            <Table.Td>{event.name}</Table.Td>
          </Table.Tr>

          <Table.Tr>
            <Table.Th>Description</Table.Th>
            <Table.Td>{event.description}</Table.Td>
          </Table.Tr>

          <Table.Tr>
            <Table.Th>Venue</Table.Th>
            <Table.Td>{event.venue}</Table.Td>
          </Table.Tr>

          <Table.Tr>
            <Table.Th>Address</Table.Th>
            <Table.Td>{event.address}</Table.Td>
          </Table.Tr>

          <Table.Tr>
            <Table.Th>Date</Table.Th>
            <Table.Td>{format(event.date, "PPPP")}</Table.Td>
          </Table.Tr>

          <Table.Tr>
            <Table.Th>Time</Table.Th>
            <Table.Td>{event.time}</Table.Td>
          </Table.Tr>

          <Table.Tr>
            <Table.Th>Dress Code</Table.Th>
            <Table.Td>{event.dressCode}</Table.Td>
          </Table.Tr>
        </Table.Tbody>
      </Table>
    </>
  );
};

export default EventView;
