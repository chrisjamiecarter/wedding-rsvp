import { Group, Table } from "@mantine/core";

import { LoadingPage } from "@/components/ui/loading-page";
import { useInvite } from "@/features/invites/api/get-invite";

import { UpdateInvite } from "./update-invite";

const InviteView = ({ inviteId }: { inviteId: string }) => {
  const inviteQuery = useInvite({
    inviteId,
  });

  if (inviteQuery.isLoading) {
    return <LoadingPage />;
  }

  const invite = inviteQuery.data;

  if (!invite) return null;

  return (
    <>
      <Group justify="end">
        <UpdateInvite inviteId={inviteId} />
      </Group>

      <Table variant="vertical">
        <Table.Tbody>
          <Table.Tr>
            <Table.Th>ID</Table.Th>
            <Table.Td>{invite.id}</Table.Td>
          </Table.Tr>

          <Table.Tr>
            <Table.Th>Household Name</Table.Th>
            <Table.Td>{invite.householdName}</Table.Td>
          </Table.Tr>

          <Table.Tr>
            <Table.Th>Email</Table.Th>
            <Table.Td>{invite.email}</Table.Td>
          </Table.Tr>
        </Table.Tbody>
      </Table>
    </>
  );
};

export default InviteView;
