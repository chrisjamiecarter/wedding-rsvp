namespace WeddingRsvp.Application.Entities;

public class Event
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }

    public string Description { get; set; } = string.Empty;

    public required string Venue { get; set; }

    public required string Address { get; set; }

    public required DateOnly Date { get; set; }

    public required TimeOnly Time { get; set; }

    public string DressCode { get; set; } = string.Empty;
}
