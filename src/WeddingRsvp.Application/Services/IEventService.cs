﻿using WeddingRsvp.Application.Entities;
using WeddingRsvp.Application.Models;

namespace WeddingRsvp.Application.Services;

public interface IEventService
{
    Task<bool> CreateAsync(Event evnt, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(DeleteEventOptions options, CancellationToken cancellationToken = default);
    Task<PaginatedList<Event>> GetAllAsync(GetAllEventsOptions options, CancellationToken cancellationToken = default);
    Task<Event?> GetAsync(GetEventOptions options, CancellationToken cancellationToken = default);
    Task<Event?> UpdateAsync(Event evnt, CancellationToken cancellationToken = default);
}