import { useParams } from "react-router";
import { ErrorBoundary } from "react-error-boundary";
import { Space } from "@mantine/core";

import { ContentLayout } from "@/components/layouts/content-layout";
import { LoadingPage } from "@/components/ui/loading-page";
import { useInvite } from "@/features/invites/api/get-invite";
import InviteView from "@/features/invites/components/invite-view";
import Guests from "@/features/guests/components/guests";

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
      <Space h="xl" />
      <ErrorBoundary
        fallback={<div>Failed to load guests. Try to refresh the page.</div>}>
        <Guests inviteId={inviteId} />
      </ErrorBoundary>
    </ContentLayout>
  );
};
export default InviteRoute;
