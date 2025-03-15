namespace WeddingRsvp.Contracts.Requests.V1.Events;

public sealed record UpdateEventRequest(string Name,
                                        string Description,
                                        string Venue,
                                        string Address,
                                        DateOnly Date,
                                        TimeOnly Time,
                                        string DressCode);
