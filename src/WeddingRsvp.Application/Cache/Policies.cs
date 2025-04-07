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

    public static class Guest
    {
        public const string Name = "GuestCache";
        public const string Tag = "guests";

        public static readonly TimeSpan Expiration = TimeSpan.FromMinutes(1);
    }

    public static class Invite
    {
        public const string Name = "InviteCache";
        public const string Tag = "invites";

        public static readonly TimeSpan Expiration = TimeSpan.FromMinutes(1);
    }
}
