namespace WeddingRsvp.Api.Endpoints.V1.FoodOptions;

public static class FoodOptionsEndpointsExtensions
{
    public static IEndpointRouteBuilder MapFoodOptionsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapCreateFoodOption();
        app.MapDeleteFoodOption();
        app.MapGetAllFoodOptions();
        app.MapGetFoodOption();
        app.MapUpdateFoodOption();

        return app;
    }
}
