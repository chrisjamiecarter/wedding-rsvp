using WeddingRsvp.Application.Models;

namespace WeddingRsvp.Application.Options;

/// <summary>
/// Defines the application seeding requirements.
/// </summary>
internal class SeederOptions
{
    public bool SeedDatabase { get; set; }

    public SeedUser? SeedUser { get; set; }
}
