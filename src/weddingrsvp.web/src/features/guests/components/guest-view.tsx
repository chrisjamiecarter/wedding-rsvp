import { Group, Table } from "@mantine/core";

import { LoadingPage } from "@/components/ui/loading-page";
import { useGuest } from "@/features/guests/api/get-guest";

import { UpdateGuest } from "./update-guest";

const GuestView = ({ guestId }: { guestId: string }) => {
  const guestQuery = useGuest({
    guestId,
  });

  if (guestQuery.isLoading) {
    return <LoadingPage />;
  }

  const guest = guestQuery.data;

  if (!guest) return null;

  return (
    <>
      <Group justify="end">
        <UpdateGuest guestId={guestId} />
      </Group>

      <Table variant="vertical">
        <Table.Tbody>
          <Table.Tr>
            <Table.Th>ID</Table.Th>
            <Table.Td>{guest.id}</Table.Td>
          </Table.Tr>

          <Table.Tr>
            <Table.Th>Invite ID</Table.Th>
            <Table.Td>{guest.inviteId}</Table.Td>
          </Table.Tr>

          <Table.Tr>
            <Table.Th>Name</Table.Th>
            <Table.Td>{guest.name}</Table.Td>
          </Table.Tr>

          <Table.Tr>
            <Table.Th>RSVP</Table.Th>
            <Table.Td>{guest.rsvpStatus}</Table.Td>
          </Table.Tr>

          <Table.Tr>
            <Table.Th>Main Food Option ID</Table.Th>
            <Table.Td>{guest.mainFoodOptionId}</Table.Td>
          </Table.Tr>

          <Table.Tr>
            <Table.Th>Dessert Food Option ID</Table.Th>
            <Table.Td>{guest.dessertFoodOptionId}</Table.Td>
          </Table.Tr>
        </Table.Tbody>
      </Table>
    </>
  );
};

export default GuestView;
