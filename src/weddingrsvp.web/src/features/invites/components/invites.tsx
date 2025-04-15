import { Group } from "@mantine/core";
import { CreateInvite } from "./create-invite";
import { InvitesList } from "./invites-list";

type InvitesProps = {
  eventId: string;
};

const Invites = ({ eventId }: InvitesProps) => {
  return (
    <>
      <Group justify="end" mt="sm">
        <CreateInvite eventId={eventId} />
      </Group>
      <InvitesList eventId={eventId} />
    </>
  );
};

export default Invites;
