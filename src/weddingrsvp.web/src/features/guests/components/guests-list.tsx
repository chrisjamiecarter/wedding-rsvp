import { useQueryClient } from "@tanstack/react-query";
import { useNavigate, useSearchParams } from "react-router";
import { useGuests } from "../api/get-guests";
import { LoadingPage } from "@/components/ui/loading-page";
import { Button, Group, Pagination, Table } from "@mantine/core";
import { Link } from "@/components/ui/link";
import { paths } from "@/configs/paths";
import DeleteGuest from "./delete-guest";
import { getGuestQueryOptions } from "../api/get-guest";

export type GuestsListProps = {
  inviteId: string;
};

export const GuestsList = ({ inviteId }: GuestsListProps) => {
  const navigate = useNavigate();
  const [searchParams] = useSearchParams();

  const guestsQuery = useGuests({
    inviteId: inviteId,
    pageNumber: +(searchParams.get("page") || 1),
  });

  const queryClient = useQueryClient();

  if (guestsQuery.isLoading) {
    return <LoadingPage />;
  }

  const pageNumber = guestsQuery.data?.pageNumber ?? 1;
  const totalPages = guestsQuery.data?.totalPages ?? 1;
  const guests = guestsQuery.data?.items;

  if (!guests) return null;

  const rows = guests.map((guest) => (
    <Table.Tr key={guest.id}>
      <Table.Td>{guest.name}</Table.Td>
      <Table.Td>{guest.rsvpStatus}</Table.Td>
      <Table.Td>{guest.mainFoodOptionName}</Table.Td>
      <Table.Td>{guest.dessertFoodOptionName}</Table.Td>
      <Table.Td>
        <Link
          onMouseEnter={() => {
            queryClient.prefetchQuery(getGuestQueryOptions(guest.id));
          }}
          to={paths.app.guest.getHref(guest.id)}>
          <Button variant="transparent">View</Button>
        </Link>
      </Table.Td>
      <Table.Td>
        <DeleteGuest guestId={guest.id} inviteId={guest.inviteId} />
      </Table.Td>
    </Table.Tr>
  ));

  function navigateToPage(value: number): void {
    const href = `${paths.app.invite.getHref(inviteId)}?page=${value}`;
    navigate(href);
  }

  return (
    <>
      <Table highlightOnHover>
        <Table.Thead>
          <Table.Tr>
            <Table.Th w="40%">Name</Table.Th>
            <Table.Th w="20%">RSVP</Table.Th>
            <Table.Th w="20%">Main</Table.Th>
            <Table.Th w="20%">Dessert</Table.Th>
            <Table.Th w="1rem"></Table.Th>
            <Table.Th w="1rem"></Table.Th>
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
