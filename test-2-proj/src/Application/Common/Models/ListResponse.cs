namespace Application.Common.Models;

public class ListResponse<T>
{
    public ListResponse(IReadOnlyCollection<T> items)
    {
        Items = items;
    }

    public IReadOnlyCollection<T> Items { get; }
}