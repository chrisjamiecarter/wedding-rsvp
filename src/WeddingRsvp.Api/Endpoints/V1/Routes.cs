﻿namespace WeddingRsvp.Api.Endpoints.V1;

public static class Routes
{
    private const string ApiBase = "api";

    public static class Events
    {
        private const string Base = $"{ApiBase}/events";

        public const string Create = Base;
        public const string Delete = $"{Base}/{{id:guid}}";
        public const string Get = $"{Base}/{{id:guid}}";
        public const string GetAll = Base;
        public const string Update = $"{Base}/{{id:guid}}";

        private const string FoodOptionsBase = $"{Base}/{{eventId:guid}}/foodoptions";

        public const string CreateFoodOption = FoodOptionsBase;
        public const string DeleteFoodOption = $"{FoodOptionsBase}/{{foodOptionId:guid}}";
        public const string GetAllFoodOptions = FoodOptionsBase;

        private const string InvitesBase = $"{Base}/{{eventId:guid}}/invites";

        public const string CreateInvite = InvitesBase;
        public const string GetAllInvites = InvitesBase;

        private const string GuestsBase = $"{Base}/{{eventId:guid}}/guests";

        public const string GetAllGuests = GuestsBase;
    }

    public static class EventFoodOptions
    {
        private const string Base = $"{ApiBase}/eventfoodoptions";

        public const string Get = $"{Base}/{{id:guid}}";
    }

    public static class FoodOptions
    {
        private const string Base = $"{ApiBase}/foodoptions";

        public const string Create = Base;
        public const string Delete = $"{Base}/{{id:guid}}";
        public const string Get = $"{Base}/{{id:guid}}";
        public const string GetAll = Base;
        public const string Update = $"{Base}/{{id:guid}}";
    }

    public static class Invites
    {
        private const string Base = $"{ApiBase}/invites";

        public const string Delete = $"{Base}/{{id:guid}}";
        public const string GenerateToken = $"{Base}/{{id:guid}}/GenerateToken";
        public const string Get = $"{Base}/{{id:guid}}";
        public const string SubmitRsvp = $"{Base}/{{id:guid}}/rsvp";
        public const string Update = $"{Base}/{{id:guid}}";

        private const string GuestsBase = $"{Base}/{{inviteId:guid}}/guests";

        public const string CreateGuest = GuestsBase;
        public const string GetAllGuests = GuestsBase;
    }

    public static class Guests
    {
        private const string Base = $"{ApiBase}/guests";

        public const string Delete = $"{Base}/{{id:guid}}";
        public const string Get = $"{Base}/{{id:guid}}";
        public const string Update = $"{Base}/{{id:guid}}";
    }
}
