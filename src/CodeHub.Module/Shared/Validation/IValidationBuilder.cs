using System.Linq.Expressions;

namespace CodeHub.Module.Shared.Validation;

public interface IValidationBuilder<T> where T : Settings, new()
{
    T Build();
    ValidationBuilder<T> SectionExists(string sectionKey);
    ValidationBuilder<T> CheckEnabled(Expression<Func<T, bool>> enabledProperty, string enabledKey);
    ValidationBuilder<T> CheckValue<TProp>(Expression<Func<T, TProp>> property, string valueKey);
}