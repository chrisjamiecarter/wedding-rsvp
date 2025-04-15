import { useQueryClient } from "@tanstack/react-query";
import { useNavigate, useSearchParams } from "react-router";
import { useInvites } from "../api/get-invites";
import { LoadingPage } from "@/components/ui/loading-page";
import { Button, Group, Pagination, Table } from "@mantine/core";
import { Link } from "@/components/ui/link";
import { paths } from "@/configs/paths";
import DeleteInvite from "./delete-invite";
import { getInviteQueryOptions } from "../api/get-invite";

export type InvitesListProps = {
  eventId: string;
};

export const InvitesList = ({ eventId }: InvitesListProps) => {
  const navigate = useNavigate();
  const [searchParams] = useSearchParams();

  const invitesQuery = useInvites({
    eventId: eventId,
    pageNumber: +(searchParams.get("invitesPage") || 1),
  });

  const queryClient = useQueryClient();

  if (invitesQuery.isLoading) {
    return <LoadingPage />;
  }

  const pageNumber = invitesQuery.data?.pageNumber ?? 1;
  const totalPages = invitesQuery.data?.totalPages ?? 1;
  const invites = invitesQuery.data?.items;

  if (!invites) return null;

  const rows = invites.map((invite) => (
    <Table.Tr key={invite.id}>
      <Table.Td>{invite.email}</Table.Td>
      <Table.Td>{invite.householdName}</Table.Td>
      <Table.Td>
        <Link
          onMouseEnter={() => {
            queryClient.prefetchQuery(getInviteQueryOptions(invite.id));
          }}
          to={paths.app.invite.getHref(invite.id)}>
          <Button variant="transparent">View</Button>
        </Link>
      </Table.Td>
      <Table.Td>
        <Button>Delete TODO</Button>
        {/* <DeleteInvite id={invite.id} /> */}
      </Table.Td>
    </Table.Tr>
  ));

  function navigateToPage(value: number): void {
    const href = `${paths.app.event.getHref(eventId)}?invitesPage=${value}`;
    navigate(href);
  }

  return (
    <>
      <Table highlightOnHover>
        <Table.Thead>
          <Table.Tr>
            <Table.Th>Email</Table.Th>
            <Table.Th>Household Name</Table.Th>
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
