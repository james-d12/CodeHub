namespace CodeHub.Domain.Shared;

public abstract record BaseRequest
{
    public required int Page { get; init; }
    public required int PageSize { get; init; }
}