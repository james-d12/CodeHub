using Microsoft.TeamFoundation.Common;

namespace CodeHub.Module.Shared.Query;

public sealed class QueryBuilder<T> : IQueryBuilder<T> where T : class
{
    private IQueryable<T> _query;

    public QueryBuilder(IEnumerable<T> source)
    {
        _query = source.AsQueryable();
    }

    public QueryBuilder<T> Where(string? value, Func<T, bool> predicate)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            _query = _query.AsEnumerable().Where(predicate).AsQueryable();
        }

        return this;
    }

    public QueryBuilder<T> Where(IEnumerable<string>? value, Func<T, bool> predicate)
    {
        if (!value.IsNullOrEmpty())
        {
            _query = _query.AsEnumerable().Where(predicate).AsQueryable();
        }

        return this;
    }

    public QueryBuilder<T> Where(Guid? value, Func<T, bool> predicate)
    {
        if (value is not null && Guid.Empty != value.Value)
        {
            _query = _query.AsEnumerable().Where(predicate).AsQueryable();
        }

        return this;
    }

    public QueryBuilder<T> Where<T1>(T1? value, Func<T, bool> predicate) where T1 : struct, Enum
    {
        if (value is not null)
        {
            _query = _query.AsEnumerable().Where(predicate).AsQueryable();
        }

        return this;
    }

    public List<T> ToList()
    {
        return [.. _query];
    }
}