import { ContentLayout } from "@/components/layouts/content-layout";
import { LoadingPage } from "@/components/ui/loading-page";
import { useGuest } from "@/features/guests/api/get-guest";
import GuestView from "@/features/guests/components/guest-view";
import { useParams } from "react-router";

const GuestRoute = () => {
  const params = useParams();
  const guestId = params.guestId as string;
  const guestQuery = useGuest({ guestId });

  if (guestQuery.isLoading) {
    return <LoadingPage />;
  }

  const guest = guestQuery.data;

  if (!guest) return null;

  return (
    <ContentLayout title={guest.name}>
      <GuestView guestId={guestId} />
    </ContentLayout>
  );
};
export default GuestRoute;
