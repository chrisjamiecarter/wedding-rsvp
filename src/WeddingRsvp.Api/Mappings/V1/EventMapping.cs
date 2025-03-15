using WeddingRsvp.Application.Entities;
using WeddingRsvp.Application.Models;
using WeddingRsvp.Contracts.Requests;
using WeddingRsvp.Contracts.Responses.V1.Events;

namespace WeddingRsvp.Api.Mappings.V1;

public static class EventMapping
{
    //public static Event ToEntity(this CreateEventRequest request)
    //{
    //    return new Event
    //    {
    //        Id = Guid.CreateVersion7(),
    //    };
    //}

    //public static Event ToEntity(this UpdateEventRequest request, Guid id)
    //{
    //    return new Event
    //    {
    //        Id = id,
    //    };
    //}

    public static EventResponse ToResponse(this Event entity)
    {
        return new EventResponse(entity.Id, entity.Name, entity.Description, entity.Venue, entity.Address, entity.Date, entity.Time, entity.DressCode);
    }

    public static EventsResponse ToResponse(this PaginatedList<Event> entities)
    {
        return new EventsResponse
        {
            Items = entities.Items.Select(ToResponse),
            PageNumber = entities.PageNumber,
            PageSize = entities.PageSize,
            TotalCount = entities.TotalCount,
            TotalPages = entities.TotalPages,
        };
    }
}
