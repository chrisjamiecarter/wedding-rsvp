﻿namespace WeddingRsvp.Contracts.Responses.V1.Rsvps;

public sealed record GetRsvpResponse(Guid Token,
                                     IEnumerable<GuestRsvpResponse> Guests,
                                     IEnumerable<FoodOptionRsvpResponse> Mains,
                                     IEnumerable<FoodOptionRsvpResponse> Desserts);
