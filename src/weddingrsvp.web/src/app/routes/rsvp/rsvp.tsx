import { useSearchParams } from "react-router";
import RsvpLayout from "@/components/layouts/rsvp-layout";
import Rsvp from "@/features/rsvps/components/rsvp";

const RsvpRoute = () => {
  const [searchParams] = useSearchParams();

  const inviteId = searchParams.get("inviteId");
  const token = searchParams.get("token");

  if (!inviteId || !token) {
    return (
      <RsvpLayout>
        <p>You need a valid invite to view this page!</p>
      </RsvpLayout>
    );
  }

  return (
    <RsvpLayout>
      <p>Hello from RSVP route</p>
      <p>Invite ID: {inviteId}</p>
      <p>Token: {token}</p>
      <Rsvp inviteId={inviteId} token={token} />
    </RsvpLayout>
  );
};

export default RsvpRoute;
