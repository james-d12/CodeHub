namespace CodeHub.Core.Models;

public sealed class Organization
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public IReadOnlyCollection<User> Users => _users;
    private readonly HashSet<User> _users = [];

    public Organization(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }

    public bool AddUser(User user)
    {
        user.AddToOrganization(this);
        return _users.Add(user);
    }

    public bool RemoveUser(User user)
    {
        user.RemoveFromOrganization(this);
        return _users.Remove(user);
    }

    public bool ContainsUser(User user) => _users.Contains(user);
}