namespace WeddingRsvp.Contracts.Responses.V1.Events;

public sealed record EventResponse(Guid Id,
                                   string Name,
                                   string Description,
                                   string Venue,
                                   string Address,
                                   DateOnly Date,
                                   TimeOnly Time,
                                   string DressCode);
