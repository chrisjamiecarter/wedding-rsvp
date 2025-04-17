import { ContentLayout } from "@/components/layouts/content-layout";
import { LoadingPage } from "@/components/ui/loading-page";
import { useInvite } from "@/features/invites/api/get-invite";
import InviteView from "@/features/invites/components/invite-view";
import { useParams } from "react-router";

const InviteRoute = () => {
  const params = useParams();
  const inviteId = params.inviteId as string;
  const inviteQuery = useInvite({ inviteId });

  if (inviteQuery.isLoading) {
    return <LoadingPage />;
  }

  const invite = inviteQuery.data;

  if (!invite) return null;

  return (
    <ContentLayout title={invite.householdName}>
      <InviteView inviteId={inviteId} />
    </ContentLayout>
  );
};
export default InviteRoute;
