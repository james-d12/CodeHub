namespace CodeHub.Shared.Query;

public interface IQueryBuilder<T> where T : class
{
    QueryBuilder<T> Where(string? value, Func<T, bool> predicate);
    QueryBuilder<T> Where(Guid? value, Func<T, bool> predicate);
}