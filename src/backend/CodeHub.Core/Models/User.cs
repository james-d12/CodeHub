namespace CodeHub.Core.Models;

public sealed record Name
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }

    private Name()
    {
    }

    public static Name Create(string firstName, string lastName)
    {
        if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
        {
            throw new ArgumentNullException(nameof(firstName), nameof(firstName) + " cannot be null or empty.");
        }

        return new Name { FirstName = firstName, LastName = lastName };
    }
}

public sealed record Email
{
    public required string Value { get; init; }

    private Email()
    {
    }

    public static Email Create(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            throw new ArgumentNullException(nameof(email), nameof(email) + " cannot be null or empty.");
        }

        if (!email.Contains('@'))
        {
            throw new ArgumentException("Invalid email format.", nameof(email));
        }

        return new Email { Value = email };
    }
}

public sealed class User
{
    public required Guid Id { get; init; }
    public required Name Name { get; set; }
    public required Email Email { get; set; }

    public Organization? Organisation { get; set; }

    public User(Name name, Email email)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
    }

    public void AddToOrganization(Organization organization)
    {
        Organisation = organization;
    }

    public void RemoveFromOrganization(Organization organization)
    {
        Organisation = null;
    }
}