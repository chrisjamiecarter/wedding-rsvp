type FoodsProps = {
  eventId: string;
};

const Foods = ({ eventId }: FoodsProps) => {
  return <>Hello from Foods: Event ID = {eventId}</>;
};

export default Foods;
