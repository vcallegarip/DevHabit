using DevHabit.Api.DTOs.Common;
using DevHabit.Api.Entities;

namespace DevHabit.Api.DTOs.Entries;

public sealed record EntriesQueryParameters : AcceptHeaderDto
{
    public string? Sort { get; init; }
    public string? Fields { get; init; }
    public string? HabitId { get; init; }
    public DateOnly? FromDate { get; init; }
    public DateOnly? ToDate { get; init; }
    public EntrySource? Source { get; init; }
    public bool? IsArchived { get; init; }
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}
