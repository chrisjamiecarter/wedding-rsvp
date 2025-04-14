import { Button, Group } from "@mantine/core";

type InvitesProps = {
  eventId: string;
};

const Invites = ({ eventId }: InvitesProps) => {
  return (
    <>
      Hello from Invites: Event ID = {eventId}
      <Group justify="end">
        <Button>Create Invite</Button>
      </Group>
    </>
  );
};

export default Invites;
