namespace WeddingRsvp.Application.Cache;

public static class Policies
{
    public static class Event
    {
        public const string Name = "EventCache";
        public const string Tag = "events";
        
        public static readonly TimeSpan Expiration = TimeSpan.FromMinutes(1);
    }

    public static class FoodOption
    {
        public const string Name = "FoodOptionCache";
        public const string Tag = "foodoptions";

        public static readonly TimeSpan Expiration = TimeSpan.FromMinutes(1);
    }

    public static class EventFoodOption
    {
        public const string Name = "EventFoodOptionCache";
        public const string Tag = "eventfoodoptions";

        public static readonly TimeSpan Expiration = TimeSpan.FromMinutes(1);
    }
}
