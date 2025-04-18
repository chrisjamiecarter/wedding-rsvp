import { Group } from "@mantine/core";
import { CreateGuest } from "./create-guest";
import { GuestsList } from "./guests-list";

type GuestsProps = {
  inviteId: string;
};

const Invites = ({ inviteId }: GuestsProps) => {
  return (
    <>
      <Group justify="end" mt="sm">
        <CreateGuest inviteId={inviteId} />
      </Group>
      <GuestsList inviteId={inviteId} />
    </>
  );
};

export default Invites;
