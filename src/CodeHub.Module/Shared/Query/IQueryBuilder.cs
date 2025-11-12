namespace CodeHub.Module.Shared.Query;

public interface IQueryBuilder<T> where T : class
{
    QueryBuilder<T> Where(string? value, Func<T, bool> predicate);
    QueryBuilder<T> Where(IEnumerable<string>? value, Func<T, bool> predicate);
    QueryBuilder<T> Where(Guid? value, Func<T, bool> predicate);
}