using AutoFixture;
using CodeHub.Module.Shared.Query;

namespace CodeHub.Module.Tests.Shared.Query;

public sealed class QueryBuilderTests
{
    private readonly Fixture _fixture;

    public QueryBuilderTests()
    {
        _fixture = new Fixture();
    }

    [Fact]
    public void Where_StringValue_FiltersCorrectly()
    {
        // Arrange
        var items = _fixture.CreateMany<TestEntity>(5).ToList();
        var expectedItem = items.First();

        // Act
        var result = new QueryBuilder<TestEntity>(items)
            .Where(expectedItem.Name, x => x.Name == expectedItem.Name)
            .ToList();

        // Assert
        Assert.Single(result);
        Assert.Equal(expectedItem, result.First());
    }

    [Fact]
    public void Where_EmptyString_DoesNotFilter()
    {
        // Arrange
        var items = _fixture.CreateMany<TestEntity>(5).ToList();

        // Act
        var result = new QueryBuilder<TestEntity>(items)
            .Where("", x => x.Name == "NonExistent")
            .ToList();

        // Assert
        Assert.Equal(items.Count, result.Count);
    }

    [Fact]
    public void Where_GuidValue_FiltersCorrectly()
    {
        // Arrange
        var items = _fixture.CreateMany<TestEntity>(5).ToList();
        var expectedItem = items.First();

        // Act
        var result = new QueryBuilder<TestEntity>(items)
            .Where(expectedItem.Id, x => x.Id == expectedItem.Id)
            .ToList();

        // Assert
        Assert.Single(result);
        Assert.Equal(expectedItem, result.First());
    }

    [Fact]
    public void Where_NullGuid_DoesNotFilter()
    {
        // Arrange
        var items = _fixture.CreateMany<TestEntity>(5).ToList();

        // Act
        var result = new QueryBuilder<TestEntity>(items)
            .Where((Guid?)null, x => x.Id == Guid.NewGuid())
            .ToList();

        // Assert
        Assert.Equal(items.Count, result.Count);
    }

    [Fact]
    public void Where_EnumValue_FiltersCorrectly()
    {
        // Arrange
        var inactiveTestEntities = _fixture
            .Build<TestEntity>()
            .With(t => t.Status, TestStatus.Inactive)
            .CreateMany(4)
            .ToList();

        var activeTestEntity = _fixture
            .Build<TestEntity>()
            .With(t => t.Status, TestStatus.Active)
            .Create();

        inactiveTestEntities.Add(activeTestEntity);

        // Act
        var result = new QueryBuilder<TestEntity>(inactiveTestEntities)
            .Where<TestStatus>(activeTestEntity.Status, x => x.Status == activeTestEntity.Status)
            .ToList();

        // Assert
        Assert.Single(result);
        Assert.Equal(activeTestEntity, result.First());
    }

    [Fact]
    public void Where_StringListProperty_FiltersCorrectly()
    {
        // Arrange
        var items = _fixture.CreateMany<TestEntity>(5).ToList();
        var filterNames = new List<string> { items.First().NamesList.First() };

        // Act
        var result = new QueryBuilder<TestEntity>(items)
            .Where(filterNames, x => x.NamesList.Intersect(filterNames).Any())
            .ToList();

        // Assert
        Assert.Single(result);
        Assert.Contains(result, x => x.NamesList.Contains(filterNames[0]));
    }

    [Fact]
    public void Where_NullStringList_DoesNotFilter()
    {
        // Arrange
        var items = _fixture.CreateMany<TestEntity>(5).ToList();

        // Act
        var result = new QueryBuilder<TestEntity>(items)
            .Where((IEnumerable<string>?)null, x => x.NamesList.Contains("NonExistent"))
            .ToList();

        // Assert
        Assert.Equal(items.Count, result.Count);
    }

    private sealed class TestEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<string> NamesList { get; set; } = [];
        public TestStatus Status { get; set; }
    }

    private enum TestStatus
    {
        Active,
        Inactive
    }
}