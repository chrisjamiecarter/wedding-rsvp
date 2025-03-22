namespace WeddingRsvp.Application.Cors;

public static class Policies
{
    public static class Web
    {
        public const string Name = "WebCors";
        public const string Origins = "http://localhost:58001";
    }
}
