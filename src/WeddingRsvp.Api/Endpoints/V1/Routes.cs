namespace WeddingRsvp.Api.Endpoints.V1;

public static class Routes
{
    private const string ApiBase = "api";

    public static class Auth
    {
        private const string Base = $"{ApiBase}/auth";

        public const string Login = $"{Base}/login";
        public const string Register = $"{Base}/register";
    }

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
        public const string DeleteFoodOption = $"{FoodOptionsBase}/{{id:guid}}";
        public const string GetAllFoodOptions = FoodOptionsBase;
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
}
